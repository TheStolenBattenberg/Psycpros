using System;
using System.Collections.Generic;

using OpenTK.Input;

namespace KF2.Component {
    class MInputManager {
        public struct KeyBind {
            private Key key;
            private MouseButton mb;

            private bool bHeld;
            private bool bReleased;
            private bool bPressed;
            
            public KeyBind(Key key) {
                mb = 0;
                this.key = key;
                bHeld = false;
                bReleased = false;
                bPressed = false;
            }
            public KeyBind(MouseButton mb) {
                this.mb = mb;
                key = 0;
                bHeld = false;
                bReleased = false;
                bPressed = false;
            }

            public MouseButton MB {
                set { mb = value; }
                get { return mb; }
            }
            public Key Key {
                set { key = value; }
                get { return key; }
            }
            public bool Held {
                set { bHeld = value; }
                get { return bHeld; }
            }
            public bool Released {
                set { bReleased = value; }
                get { return bReleased; }
            }
            public bool Pressed {
                set { bPressed = value; }
                get { return bPressed; }
            }
        }

        private Dictionary<string, KeyBind> dKeyBind;

        bool bThreaded = false;

        public MInputManager(bool threaded) {
            dKeyBind   = new Dictionary<string, KeyBind>();

            bThreaded = threaded;

            if(bThreaded) {
                bThreaded = false;
                throw (new Exception("Threaded Input not supported."));
            }
        }

        //Update method
        public void Update() {
            //If the Input Manager is using threads, exit to avoid errors.
            if(bThreaded == true) {
                return;
            }

            KeyboardHandler();

        }

        //Handles Keyboard Input
        private void KeyboardHandler() {
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            Dictionary<string, KeyBind> bindValues = new Dictionary<string, KeyBind>();

            foreach (KeyValuePair<string, KeyBind> keyBind in dKeyBind) {
                KeyBind key = keyBind.Value;

                //Check Keyboard Binds
                if (key.Key != 0 && keyState.IsConnected) {
                    //See if the key was released
                    if (key.Held == true && keyState.IsKeyUp(key.Key)) {
                        key.Released = true;
                    }

                    //See if the key was pressed
                    if (key.Held == false && keyState.IsKeyDown(key.Key)) {
                        key.Pressed = true;
                    }

                    //If the key is held.
                    key.Held = keyState.IsKeyDown(key.Key);

                    //Update Directory with Copy
                    bindValues[keyBind.Key] = key;
                }

                //Check Mouse Binds
                if(key.MB != 0) {

                }
            }

            //Switch the bind values to the updated dictionary
            dKeyBind.Clear();
            dKeyBind = bindValues;
        }

        //Add Binds with these
        public void AddBind(string name, Key key) {
            dKeyBind.Add(name, new KeyBind(key));
        }
        public void AddBind(string name, MouseButton mb) {
            dKeyBind.Add(name, new KeyBind(mb));
        }

        //Check binds with these
        public bool GetPressed(string name) {
            return dKeyBind[name].Pressed;
        }
        public bool GetReleased(string name) {
            return dKeyBind[name].Released;
        }
        public bool GetHeld(string name) {
            return dKeyBind[name].Held;
        }
    }
}
