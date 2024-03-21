using System.Collections;
using System.Collections.Generic;

namespace AngusChanToolket.UI_DynamicBar
{
    public class UIDynamicBars
    {
        Dictionary<IUIDynamicTrack, IUIDynamicBar> activeBars = new Dictionary<IUIDynamicTrack, IUIDynamicBar>();
        Queue<IUIDynamicBar> pool = new Queue<IUIDynamicBar>();

        public UIDynamicBars(IUIDynamicBar[] barInstances)
        {
            for (int i = 0; i < barInstances.Length; i++)
            {
                IUIDynamicBar bar = barInstances[i];
                bar.SetActive(false);

                pool.Enqueue(bar);
            }
        }
        public void ActiveBar(IUIDynamicTrack track)
        {
            if (activeBars.TryGetValue(track, out IUIDynamicBar bar))
            {
                bar.SetActive(true);
            }
            else
            {
                if (pool.Count > 0)
                {
                    bar = pool.Dequeue();
                    bar.SetActive(true);
                    activeBars.Add(track, bar);
                }
            }
        }
        public void DeactiveBar(IUIDynamicTrack track)
        {
            if (activeBars.TryGetValue(track, out IUIDynamicBar bar))
            {
                bar.SetActive(false);
                activeBars.Remove(track);
                pool.Enqueue(bar);
            }
        }

        public void Update(float delta)
        {
            foreach (var item in activeBars)
            {
                item.Value.SetPosition(item.Key.PositionX, item.Key.PositionY, item.Key.PositionZ);
                item.Value.SetProgress(item.Key.CurrentValue / item.Key.MaxValue);
            }
        }
    }
}
