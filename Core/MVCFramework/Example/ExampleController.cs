using System.Collections;
using System.Collections.Generic;

namespace AngusChanToolkit.MVC.Example
{
    public class ExampleController
    {
        Controller controller;

        public void Initial()
        {
            IModel[] models = new IModel[] { new ExampleModel() };
            IView[] views = new IView[] { new ExampleView() };

            controller = new Controller(models, views);
        }
    }
}