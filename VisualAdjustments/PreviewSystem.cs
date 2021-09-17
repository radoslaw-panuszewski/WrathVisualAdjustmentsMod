using System;
using System.Linq;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.MVVM._PCView.InGame;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.Inventory;
using Kingmaker.UI.MVVM._VM.ServiceWindows.Inventory;
using Kingmaker.UI.ServiceWindow;
using Kingmaker.UnitLogic;
using Kingmaker.Visual.CharacterSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace VisualAdjustments
{
    public static class PreviewSystem
    {
        public static Camera camera;
        public static RenderTexture DollroomRenderTexture = new RenderTexture(800,800,1);
        //public static Camera cam;
        public static Texture texture
        {
            get
            {
                if (m_texture == null)
                {
                    Instantiate();
                }
                m_texture = DollroomRenderTexture;
                return DollroomRenderTexture;
            }
        }

        private static Texture m_texture;
        public static void Instantiate()
        {
            /*  var n = new InventoryDollPCView();
              unitdesc
              var asd = IObservable<out UnitDescriptor asd>();
              var dollvm = new InventoryDollVM(new ReadOnlyReactiveProperty<UnitDescriptor>((Game.Instance.Player.AllCharacters.First())),new BoolReactiveProperty(true));
              n.Bind();*/

            /*GameObject.Instantiate(UnityEngine.Object.FindObjectOfType<InGamePCView>().m_StaticPartPCView
                .m_ServiceWindowsPCView.m_InventoryPCView.m_DollView);*/
            var gameobject = new GameObject("previewtex");
            var asdas = gameobject.AddComponent<RawImage>();
          
          var dollroom = gameobject.AddComponent<DollRoomCharacterController>();
          var dollroom2 = gameobject.AddComponent<DollRoom>();
          dollroom2.OnEnable();
          dollroom2.m_Avatar = Game.Instance.Player.PartyAndPets.First().View.CharacterAvatar;
          var dollcam = dollroom2.m_Camera = new DollCamera();
          dollcam.m_IsInit = true;
          dollcam.LookAt(dollroom2.m_Avatar.transform);
          //dollroom2.m_Camera.Start();
          var cam = gameobject.AddComponent<Camera>();
          cam.targetTexture = DollroomRenderTexture;
          gameobject.transform.position = new Vector3((float)999.85, (float)585.9939, (float)-1);
          gameobject.transform.localPosition = new Vector3((float)0, (float)2.124, (float)-1);
          gameobject.transform.rotation = new Quaternion((float)7.69, 180, 0,0);
          //cam.CopyFrom(tempcam.Where(a => a.GetComponent<DollCamera>() != null).First(););
            
          cam.enabled = true;
          dollroom.Character = Game.Instance.Player.PartyAndPets.First().View.CharacterAvatar.transform;
          dollroom.Update();
          asdas.texture = DollroomRenderTexture;
          m_texture = asdas.texture;
          camera = cam;
        }
    }
}