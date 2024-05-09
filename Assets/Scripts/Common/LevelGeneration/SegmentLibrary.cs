using UnityEngine;
using Random = System.Random;

namespace SpaceDrifter2D
{
    [CreateAssetMenu(fileName ="SegmentLibrary", menuName = "ScriptableObjects/SegmentLibrary")]
    public class SegmentLibrary : ScriptableObject
    { 
        [SerializeField] Segment_MB[] segments;
        [SerializeField] Segment_MB[] exits;
        [SerializeField] SegmentPlacementRule[] rules;
         
        private Random r;
        public void Init(int seed) // seed for repeatability
        {
            r = new Random(seed);
        }

        public Segment_MB SpawnSegment(Segment_MB prev_segment, Transform parent)
        {
            Segment_MB new_segment;
            do
            {
                new_segment = segments[r.Next(segments.Length)];
            } while (!ComplyRules(new_segment, prev_segment));
            return Instantiate(new_segment, parent);
        }
        public Segment_MB SpawnExit(Segment_MB prev_segment, Transform parent)
        {
            Segment_MB new_segment;
            do
            {
                new_segment = exits[r.Next(exits.Length)];
            } while (!ComplyRules(new_segment, prev_segment));
            return Instantiate(new_segment, parent);
        }

        private bool ComplyRules(Segment_MB segment, Segment_MB prev)
        {
            foreach( var r in rules )
            {
                if (!r.Comply(segment, prev))
                    return false;
            }
            return true;
        }
    }

    public enum EntranceType { wide, narrow, none }
    public enum DirectionChange { Keep = 0, Left = 90, Right = -90, BackLeft = 180, BackRight = -180 }
}