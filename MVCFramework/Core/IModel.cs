using System.Collections;
using System.Collections.Generic;

namespace AngusChanToolkit.MVC
{
    public interface IModel
    {
        void Initial(Context context);
        void Update(Context context, float timeStep);
    }
}