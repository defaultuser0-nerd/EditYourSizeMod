using UnityEngine;
using BepInEx;
using System;
using Newtilla;

namespace EditYourSizeMod
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private bool inRoom;
        private float size = 1;

        void Start()
        {
            Newtilla.Newtilla.OnJoinModded += OnModdedJoined;
            Newtilla.Newtilla.OnLeaveModded += OnModdedLeft;
        }

        private void OnDisable()
        {
            GorillaLocomotion.Player.Instance.scale = 1f;
            size = 1f;
        }

        void OnModdedJoined(string modeName)
        {
            inRoom = true;
        }

        void OnModdedLeft(string modeName)
        {
            inRoom = false;
            GorillaLocomotion.Player.Instance.scale = 1f;
            size = 1f;
        }

        private void Update()
        {
            if (inRoom)
            {
                GorillaLocomotion.Player.Instance.scale = size;
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0)
                {
                    size += 0.05f;
                }

                if (ControllerInputPoller.instance.leftControllerIndexFloat > 0)
                {
                    size -= 0.05f;
                    if (size < 0.05f)
                    {
                        size = 0.05f;
                    }
                }
                if (ControllerInputPoller.instance.leftControllerPrimaryButton)
                {
                    size = 1f;
                }
            }
        }
    }
}
