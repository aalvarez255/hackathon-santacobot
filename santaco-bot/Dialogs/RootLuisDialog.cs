namespace LuisBot.Dialogs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;
    using Microsoft.Bot.Connector;
    using santaco_bot.Services;

    [LuisModel("1510f8cf-c0f6-41d3-97d3-ba3589ff1ffb", "7cb8ee90c4754d79b0b8d63b1343b6d3", domain: "westus.api.cognitive.microsoft.com")]
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {
        private string lastQuery = "";

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            lastQuery = "";
            string[] notUnderstandingInterjections = new string[] {
                "A ver... no te estoy entendiendo... Me lo podrias volver a repetir? Despacito, que me entere.",
                "Ups... No te entiendo eh. Intentalo otra vez.",
                "Sé que soy un poquito pesadito pero es que no sé lo que me dices. Repite anda.",
                "Eh... ¿Cómo dices? Non capisco amigo.",
                };

            string[] canAskForHelpMessages = new string[] {
                "Si me pongo muy pesado siempre puedes pedirme ayuda.",
                "Di 'ayuda' si no consigues que te entienda.",
                "Recuerda que siempre puedes decir 'ayuda' si no te entiendo.",
                "Si es necesario me puedes pedir ayuda.",
                };

            int notUnderstandingIndex = new Random().Next(1, notUnderstandingInterjections.Length);
            int askHelpIndex = new Random().Next(1, notUnderstandingInterjections.Length);

            string response = $"{notUnderstandingInterjections[notUnderstandingIndex]} {canAskForHelpMessages[askHelpIndex]}";
            await context.PostAsync(response);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Tipo de GIF")]
        public async Task Search(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            string[] incoming = new string[] {
                "Marchando.",
                "¿Qué te parece este?",
                "Un gif calentito para mi interlocutor favorito:",
                "Voilà!",
                "¡Aquí tienes!",
                "Ahí lo llevas:",
                "jajajaja en serio? Venga toma",
                "Ya estás pidiendo gifs? Qué poco te veo trabajar...",
                "A ti no puedo decirte que no",
                "Hmmm... Curiosa query la tuya",
                "Hay que ver, te gustan más los gifs que a un tonto un lápiz.",
                "Ahí va tu gif",
                };

            int incomingIndex = new Random().Next(1, incoming.Length);

            TenorClient client = new TenorClient();
            string gifQuery = string.Join(" ", result.Entities.Select(e => e.Entity));
            lastQuery = gifQuery;

            string gifUrl = await client.GetGifUrl(gifQuery);
            string response = $"{incoming[incomingIndex]} {gifUrl} (un gif de '{gifQuery}')";
            await context.PostAsync(response);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Ayuda")]
        public async Task Help(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            lastQuery = "";
            string message = $"¡Hola! Soy el santaco-bot y he venido directito desde la periferia para surtirte con los mejores gifs." +
                $" De momento solo hablo castellano (¿no te he dicho que soy de Santaco?) " +         
                $" Soy todavia muy joven pero te puedo saludar, " +
                $"me puedes pedir gifs y si no te entiendo pues te pido educadamente que me lo repitas. " +
                $"Así las gastamos en Santaco. Prueba a pedirme un gif diciendo algo como 'gif de patatas'";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Saludos")]
        public async Task Hello(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            lastQuery = "";
            string[] salutations = new string[] {
                "¡Buenos dias!",
                "¡¿Qué pasa niño?!",
                "¡Hombre! Tú por aquí? Me alegro de verte",
                "¿Qué marcha llevas?",
                "¿Cómo va?",
                "¿Cómo lo llevas?",
                "¡Hombre! Hay que ver la buena cara que traes hoy!",
                "¿Tú por aquí?",
                "¡Eeeeei! ¡Qué ganas tenia de verte!",
                "Aquí el santaco-bot listo para escuchar tus deseos."
                };
            int messageIndex = new Random().Next(1, salutations.Length);
            string response = $"{salutations[messageIndex]}";
            await context.PostAsync(response);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Dame otro")]
        public async Task Other(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            string response = "";
            if (string.IsNullOrEmpty(lastQuery))
            {
                string[] vacilate = new string[] {
                "Pero si no me has pedido nada... No me vaciles",
                "¿Otro que? Si es la primera vez que me dices algo.",
                "¿Perdon? Me hablas a mi? ¡Todavía no me has pedido ningun gif!",
                "Anda no me vaciles, que aun no has dicho nada.",
                };

                int vacilateIndex = new Random().Next(1, vacilate.Length);
                response = $"{vacilate[vacilateIndex]}";
            }
            else
            {
                string[] another = new string[] {
                "¿No te ha gustado? Toma otro",
                "Errar es humano y de bots también... Supongo. Toma:",
                "Otro, otro!",
                "¿No era ese? ¿Este mejor?",
                "Ok ok... ¿Y este?",
                "Ups...sorry. ¿Este quizás?",
                "Te busco otro, te busco otro...",
                "Siempre hay un gif más",
                "Venga, ahí va otro",
                "¿No te va? ¿Y este?",
                "Vale, creo que este es incluso mejor:",
                "Ahí tienes otro:",
                };

                int anotherIndex = new Random().Next(1, another.Length);
                TenorClient client = new TenorClient();
                string gifUrl = await client.GetGifUrl(lastQuery);
                response = $"{another[anotherIndex]} {gifUrl}";
            } 
            
            await context.PostAsync(response);
            context.Wait(this.MessageReceived);
        }

    }
}
