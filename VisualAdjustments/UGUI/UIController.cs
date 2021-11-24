using Kingmaker.PubSubSystem;
using ModMaker.Utility;
using static TutorialCanvas.Main;

namespace TutorialCanvas.UI
{
    internal class UIController : ModMaker.IModEventHandler, IAreaHandler
    {
        public int Priority => 400; //ignore, ModMaker stuff.

        public UIManager TutorialUI { get; private set; }

        public void Attach()
        {
            if (TutorialUI == null)
                TutorialUI = UIManager.CreateObject();
        }

        public void Detach()
        {
            TutorialUI.SafeDestroy();
            TutorialUI = null;
        }

        public void Update()
        {
            Detach();
            Attach();
        }

#if DEBUG

        public void Clear()
        {
            Transform transform;
            while (transform = Game.Instance.UI.Common.transform.Find(UIManager.Source))
            {
                transform.SafeDestroy();
            }
            transform = null;
        }

#endif

        public void HandleModEnable() //ModMaker event from IModEventHandler
        {
            Mod.Core.UI = this;
            Attach();

            EventBus.Subscribe(this); //IMPORTANT: this subscribes to the WotR eventbus, will not work if not subscribed.
        }

        public void HandleModDisable() //ModMaker event from IModEventHandler
        {
            EventBus.Unsubscribe(this);
            Detach();
            Mod.Core.UI = null;
        }

        public void OnAreaBeginUnloading()
        {
        }

        public void OnAreaDidLoad() //IMPORTANT: event from IAreaHandler.  Needs to be subscribed to eventbus to trigger.  The UI is remade every scene load, this will attach the object after loading.
        {
            Attach();
        }
    }
}