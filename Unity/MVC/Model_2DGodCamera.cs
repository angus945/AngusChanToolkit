using UnityEngine;

namespace AngusChanToolkit.MVC.Common
{
    [System.Serializable]
    public class Model_2DGodCamera : IModel
    {
        public Camera camera;

        public bool scrollScale;
        public bool dragMove;
        public int dragMoveMouse;

        public bool keyboardMove;
        public float speed;

        Vector2 mousePos;

        void IModel.Initial(Context context)
        {

        }
        void IModel.Update(Context context, float timeStep)
        {
            if (scrollScale)
            {
                float scroll = -Input.mouseScrollDelta.y;
                camera.orthographicSize += (camera.orthographicSize * scroll / 2) * 0.5f;
            }
            if (dragMove && Input.GetMouseButtonDown(dragMoveMouse))
            {
                mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            }
            if (dragMove && Input.GetMouseButton(dragMoveMouse))
            {
                Vector2 current = camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 offset = mousePos - current;

                camera.transform.Translate(offset);
                mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            }
            if (keyboardMove)
            {
                Vector2 offset = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * timeStep;
                camera.transform.Translate(offset);
            }
        }
    }
}
