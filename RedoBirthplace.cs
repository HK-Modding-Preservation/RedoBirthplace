using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using HutongGames.PlayMaker;
using Modding;
using SFCore;
using SFCore.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace RedoBirthplace
{
    internal class RedoBirthplace : Mod
    {
        internal static RedoBirthplace Instance;

        public RedoBirthplace() : base("Redo Birthplace")
        {
            InitCallbacks();
        }

        // Thx to 56
        public override string GetVersion() => SFCore.Utils.Util.GetVersion(Assembly.GetExecutingAssembly());

        public override void Initialize()
        {
            Log("Initializing");
            Instance = this;

            Log("Initialized");
        }

        private void InitCallbacks()
        {
            // Hooks
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnSceneChanged(Scene from, Scene to)
        {
            if (to.name == "Abyss_15")
            {
                #region Dream Enter Abyss

                var dreamEnterAbyssGo = to.FindRoot("Dream Enter Abyss");
                var controlFsm = dreamEnterAbyssGo.LocateMyFSM("Control");
                var initState = controlFsm.FsmStates.First(x => x.Name == "Init");
                var actions = new List<FsmStateAction>(initState.Actions);
                actions.RemoveAt(2);
                initState.Actions = actions.ToArray();

                #endregion

                #region Mirror

                var mirrorGo = to.FindRoot("Mirror");
                var mirrorFsm = mirrorGo.LocateMyFSM("FSM");
                var mirrorCheckState = mirrorFsm.FsmStates.First(x => x.Name == "Check");
                var mirrorCheckActions = new List<FsmStateAction>(mirrorCheckState.Actions);
                mirrorCheckActions.RemoveAt(0);
                mirrorCheckState.Actions = mirrorCheckActions.ToArray();

                #endregion
            }
        }
    }
}