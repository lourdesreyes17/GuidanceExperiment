using System.Collections.Generic;
using UnityEngine;

namespace UXF
{
    public class CueTracker : Tracker
    {
        public override string MeasurementDescriptor => "guidance_cue";
        public override IEnumerable<string> CustomHeader => new string[] { "cue_num"};

        private ProvideCue provideCue;

        void Awake()
        {
            provideCue = GetComponent<ProvideCue>();
        }
        /// <summary>
        /// Returns current mouse position in screen coordinates
        /// </summary>
        /// <returns></returns>
        protected override UXFDataRow GetCurrentValues()
        {
            // get position and rotation
            string c = new string(provideCue.guidanceCue);


            // return cue as int
            var value = new UXFDataRow()
            {
                ("cue_num", c)
            };

            return value;
        }

    }

}
