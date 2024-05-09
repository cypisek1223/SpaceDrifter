using System;
using UnityEngine;

namespace SpaceDrifter2D
{
    [CreateAssetMenu(fileName ="SegPlaceRule", menuName ="ScriptableObject/SegPlaceRule")]
    public class SegmentPlacementRule : ScriptableObject
    {
        public bool Comply(Segment_MB segment, Segment_MB prev)
        {
            return EntrancesFit(segment.TypeIn, prev.TypeOut) && DontCross(segment.DirectionChange, prev);
        }

        private bool EntrancesFit(EntranceType new_in, EntranceType prev_out)
        {
            return prev_out == new_in;
        }

        //to rethink later
        /// <summary>
        /// Currently assuming that there can be max 3 nodes loaded at a time and path can cross reaching further than that     
        /// </summary>
        /// <param name="new_dir">New spawned node dir</param>
        /// <param name="prev">Previous node / segment</param>
        /// <returns></returns>
        private bool DontCross(DirectionChange new_dir, Segment_MB prev)
        {
            if (new_dir == DirectionChange.Keep)
                return true;

            if (Mathf.Abs((int)new_dir) == 180) // turn back
            {
                if(new_dir != prev.DirectionChange) // different turn back than previously
                {
                    if (new_dir == DirectionChange.BackLeft && prev.DirectionChange != DirectionChange.Left) // left turn back can't follow left turn
                    {
                        return true;
                    }
                    else if (new_dir == DirectionChange.BackRight && prev.DirectionChange != DirectionChange.Right)  //right turn can't precede right turn back
                    {
                        return true;
                    }
                }
            }
            else //not turn back
            {
                if (new_dir != prev.DirectionChange)  // if direction changes 
                {
                    return true;
                }
                else if (prev.DirectionChange != prev.Previous.DirectionChange) // direction changed previously (now stays same)
                {
                    return true;
                }
            }
            return false;
        }
    }
}