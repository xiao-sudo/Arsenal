using UnityEngine;
using UnityEngine.InputSystem;


namespace Examples
{
    public static class DemoInput
    {
        /// <summary>
        /// Current screen mouse position.
        /// </summary>
        public static Vector2 MousePosition
#if ENABLE_INPUT_SYSTEM
            => Mouse.current.position.ReadValue();
#else
            => UnityEngine.Input.mousePosition;
#endif


        /// <summary>
        /// The amount that the mouse has moved since last frame.
        /// </summary>
        public static Vector2 MousePositionDelta
#if ENABLE_INPUT_SYSTEM
            => Mouse.current.delta.ReadValue();
#else
            => new Vector2(UnityEngine.Input.GetAxisRaw("Mouse X") * 20, UnityEngine.Input.GetAxisRaw("Mouse Y") * 20);
#endif

        /// <summary>
        /// The amount that the mouse scroll value has changed since last frame.
        /// </summary>
        public static Vector2 MouseScrollDelta
#if ENABLE_INPUT_SYSTEM
            => Mouse.current.scroll.ReadValue() * 0.01f;
#else
            => UnityEngine.Input.mouseScrollDelta;
#endif


        /// <summary>
        /// Is the left mouse button pressed this frame
        /// </summary>
        public static bool LeftMouseDown
#if ENABLE_INPUT_SYSTEM
            => Mouse.current.leftButton.wasPressedThisFrame;
#else
            => UnityEngine.Input.GetMouseButtonDown(0);
#endif

        /// <summary>
        /// Is the left mouse button currently being held down
        /// </summary>
        public static bool LeftMouseHold
#if ENABLE_INPUT_SYSTEM
            => Mouse.current.leftButton.isPressed;
#else
            => UnityEngine.Input.GetMouseButton(0);
#endif

        /// <summary>
        /// Is the left mouse button released this frame
        /// </summary>
        public static bool LeftMouseUp
#if ENABLE_INPUT_SYSTEM
            => Mouse.current.leftButton.wasReleasedThisFrame;
#else
            => UnityEngine.Input.GetMouseButtonUp(0);
#endif

        /// <summary>
        /// Is the right mouse button pressed this frame
        /// </summary>
        public static bool RightMouseDown
#if ENABLE_INPUT_SYSTEM
            => Mouse.current.rightButton.wasPressedThisFrame;
#else
            => UnityEngine.Input.GetMouseButtonDown(1);
#endif


        /// <summary>
        /// Is the right mouse button currently being held down
        /// </summary>
        public static bool RightMouseHold
#if ENABLE_INPUT_SYSTEM
            => Mouse.current.rightButton.isPressed;
#else
            => UnityEngine.Input.GetMouseButton(1);
#endif

        /// <summary>
        /// Is the right mouse button released this frame
        /// </summary>
        public static bool RightMouseUp
#if ENABLE_INPUT_SYSTEM
            => Mouse.current.rightButton.wasReleasedThisFrame;
#else
            => UnityEngine.Input.GetMouseButtonUp(1);
#endif


        /// <summary>
        /// Is Space pressed this frame
        /// </summary>
        public static bool SpaceDown
#if ENABLE_INPUT_SYSTEM
            => Keyboard.current.spaceKey.wasPressedThisFrame;
#else
            => UnityEngine.Input.GetKeyDown(KeyCode.Space);
#endif


        /// <summary>
        /// Is Space currently being held down
        /// </summary>
        public static bool SpaceHold
#if ENABLE_INPUT_SYSTEM
            => Keyboard.current.spaceKey.isPressed;
#else
            => UnityEngine.Input.GetKey(KeyCode.Space);
#endif

        /// <summary>
        /// Is Space released this frame
        /// </summary>
        public static bool SpaceUp
#if ENABLE_INPUT_SYSTEM
            => Keyboard.current.spaceKey.wasReleasedThisFrame;
#else
            => UnityEngine.Input.GetKeyUp(KeyCode.Space);
#endif

#if ENABLE_INPUT_SYSTEM
        private static InputAction WASD_ACTION;
#endif

        public static Vector2 WASD
#if ENABLE_INPUT_SYSTEM
        {
            get
            {
                if (null == WASD_ACTION)
                {
                    WASD_ACTION = new InputAction();
                    WASD_ACTION.AddCompositeBinding("2DVector")
                        .With("Up", "<Keyboard>/w")
                        .With("Down", "<Keyboard>/s")
                        .With("Left", "<Keyboard>/a")
                        .With("Right", "<Keyboard>/d");

                    WASD_ACTION.Enable();
                }

                return WASD_ACTION.ReadValue<Vector2>();
            }
        }
#else
            => new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
#endif
    }
}