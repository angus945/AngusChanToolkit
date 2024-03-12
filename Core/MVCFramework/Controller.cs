using System.Collections;
using System.Collections.Generic;

namespace AngusChanToolkit.MVC
{
    public class Controller
    {
        Context context;

        IModel[] models;
        IView[] views;

        public Controller(IModel[] models, IView[] views) : this(models, views, new Context()) { }
        public Controller(IModel[] models, IView[] views, Context context)
        {
            this.context = context;

            this.models = models;
            this.views = views;

            for (int i = 0; i < models.Length; i++)
            {
                models[i].Initial(context);
            }

            for (int i = 0; i < views.Length; i++)
            {
                views[i].Initial(context);
            }
        }
        public void Update(float timeStep)
        {
            for (int i = 0; i < models.Length; i++)
            {
                models[i].Update(context, timeStep);
            }

            for (int i = 0; i < views.Length; i++)
            {
                views[i].Update(context, timeStep);
            }
        }
    }
}
