namespace LuisBot.Dialogs
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;
    using Microsoft.Bot.Connector;

    [LuisModel("1510f8cf-c0f6-41d3-97d3-ba3589ff1ffb", "7cb8ee90c4754d79b0b8d63b1343b6d3", domain: "westus.api.cognitive.microsoft.com")]
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
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

        [LuisIntent("Pedir GIF")]
        public async Task Search(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            string message = $"Entered intent pedir gif";


            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Ayuda")]
        public async Task Help(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            string message = $"¡Hola! Soy el santaco-bot y he venido directito desde la periferia para surtirte con los mejores gifs." +
                $" Soy todavia muy joven pero te puedo saludar, " +
                $"me puedes pedir gifs y si no te entiendo pues te contesto. " +
                $"Así las gastamos en Santaco. Pruaba a pedirme un gif diciendo algo como 'dame un gif de patatas'";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Saludos")]
        public async Task Hello(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
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
            string message = $"Entered intent other";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

    }
}
