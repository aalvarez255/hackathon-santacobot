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
            string message = $"Entered intent none";

            await context.PostAsync(message);

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
            string message = $"Entered intent ayuda";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Saludos")]
        public async Task Hello(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            string message = $"Entered intent salutations";

            await context.PostAsync(message);

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
