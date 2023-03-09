using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UXF
{
    public class PositionVelocityTracker : Tracker
    {
        public override string MeasurementDescriptor => "raw_kinematics";
        public override IEnumerable<string> CustomHeader => new string[] { "raw_pos_x", "raw_pos_y", "raw_pos_z", "raw_vel_x", "raw_vel_y", "raw_vel_z", "raw_rot_x", "raw_rot_y", "raw_rot_z", };

        private HapticPlugin hapticPlugin;

        void Awake()
        {
            hapticPlugin = GetComponent<HapticPlugin>();
        }

        /// <summary>
        /// Returns current position and rotation values
        /// </summary>
        /// <returns></returns>
        protected override UXFDataRow GetCurrentValues()
        {
            // get position and rotation
            Vector3 p = hapticPlugin.stylusPositionWorld;
            Vector3 v = hapticPlugin.stylusVelocityWorld;
            Quaternion r = hapticPlugin.stylusRotationWorld;

            // return position, rotation (x, y, z) as an array
            var values = new UXFDataRow()
            {
                ("raw_pos_x", p.x),
                ("raw_pos_y", p.y),
                ("raw_pos_z", p.z),
                ("raw_vel_x", v.x),
                ("raw_vel_y", v.y),
                ("raw_vel_z", v.z),
                ("raw_rot_x", r.x),
                ("raw_rot_y", r.y),
                ("raw_rot_z", r.z)
            };

            return values;
        }
    }
}
