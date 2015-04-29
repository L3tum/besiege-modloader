﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace spaar
{
    class Console : MonoBehaviour
    {

        private List<string> logMessages;
        private readonly int maxLogMessages = 150;

        private Rect windowRect;
        private Vector2 scrollPosition;

        private GUIStyle style;
        private Rect scrollViewRect;
        private bool visible = false;

        void OnEnable()
        {
            Application.RegisterLogCallback(HandleLog);
            logMessages = new List<string>(maxLogMessages);
            windowRect = new Rect(50f, 50f, 600f, 600f);
        }

        void OnDisable()
        {
            Application.RegisterLogCallback(null);
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) &&  Input.GetKeyDown(KeyCode.K))
            {
                visible = !visible;
            }
        }

        void OnGUI()
        {
            if (visible)
            {
                GUI.skin = ModLoader.GUISkin;
                windowRect = GUI.Window(-1001, windowRect, OnWindow, "Console");
            }
        }

        void OnWindow(int windowId)
        {
            float lineHeight = GUI.skin.box.lineHeight;

            var skin = GUI.skin;
            style = skin.textArea;

            GUILayout.BeginArea(new Rect(5f, lineHeight + 5f, windowRect.width - 10f, windowRect.height - 50f));
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            string logText = "";
            foreach (var s in logMessages)
            {
                logText += s + "\n";
            }
            GUILayout.TextArea(logText);
            GUILayout.EndScrollView();
            scrollViewRect = GUILayoutUtility.GetLastRect();

            GUILayout.TextField("Not yet implemented");

            GUILayout.EndArea();


            GUI.DragWindow();
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            var typeString = "[";
            switch (type)
            {
                case LogType.Assert:
                    typeString += "Assert";
                    break;
                case LogType.Error:
                    typeString += "Error";
                    break;
                case LogType.Exception:
                    typeString += "Exception";
                    break;
                case LogType.Log:
                    typeString += "Log";
                    break;
                case LogType.Warning:
                    typeString += "Warning";
                    break;
            }
            typeString += "] ";

            var logMessage = "";
            if (type == LogType.Exception)
            {
                logMessage = typeString + logString + "\n" + stackTrace;
            }
            else
            {
                logMessage = typeString + logString;
            }

            if (logMessages.Count < maxLogMessages)
            {
                logMessages.Add(logMessage);
            }
            else
            {
                logMessages.RemoveAt(0);
                logMessages.Add(logMessage);
            }

            string logText = "";
            foreach (var s in logMessages)
            {
                logText += s + "\n";
            }
            scrollPosition.y = style.CalcHeight(new GUIContent(logText), scrollViewRect.width) - scrollViewRect.height;
        }

    }
}