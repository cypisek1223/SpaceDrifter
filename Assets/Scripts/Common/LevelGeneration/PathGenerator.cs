using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceDrifter2D
{
    public class PathGenerator : MonoBehaviour
    {
        public Segment_MB startSegment;
        public Segment_MB current_segment;
        

        public SegmentLibrary library;
        public Transform parent;

        public int loaded_segment_count = 3;

        private Queue<Segment_MB> destroy_queue = new Queue<Segment_MB>();

        int playerSegmentPosition = 0;

        LevelData levelData = null;

        [SerializeField] UnityEvent OnEndReached;

        private void Start()
        {
            levelData = LevelLoader.Instance?.CurrentLevelData;

            destroy_queue.Enqueue(startSegment);
            current_segment = startSegment;

            
            library.Init(levelData!=null?levelData.Seed:1);
            //ZMIENIONE
            //library.Init(library.segments.Length);

            for (int i=0; i<loaded_segment_count-1; i++)
            {
                PlaceSegment();
            }

        }

        public void PlaceSegment()
        {
             Segment_MB new_segment = library.SpawnSegment(current_segment, parent);
            new_segment.Previous = current_segment;
            new_segment.SetRotation(current_segment.transform.rotation, current_segment.DirectionChange);
            new_segment.PlaceByEntranceAt(current_segment.Exit);
            current_segment = new_segment;
            current_segment.ParentGenerator = this;

            destroy_queue.Enqueue(current_segment);
            if(destroy_queue.Count > loaded_segment_count)
            {
                //CYPRIAN COMMENT IT
                //Destroy(destroy_queue.Dequeue().gameObject);
            }
        }

        void PlaceExit()
        {
            Segment_MB exit = library.SpawnExit(current_segment, parent);
            exit.Previous = current_segment;
            exit.SetRotation(current_segment.transform.rotation, current_segment.DirectionChange);
            exit.PlaceByEntranceAt(current_segment.Exit);
            current_segment = exit;
            current_segment.ParentGenerator = this;

            destroy_queue.Enqueue(current_segment);
            if (destroy_queue.Count > loaded_segment_count)
            {
                //CYPRIAN COMMENT IT
                //Destroy(destroy_queue.Dequeue().gameObject);
            }
        }

        public void MovePlayer()
        {
            playerSegmentPosition++;                                             //CYPRIAN ADD THIS
            if(levelData != null && levelData.Length == playerSegmentPosition || library.indexSegment >= library.segments.Length)
            {
                PlaceExit();
                return;
            }
            if(playerSegmentPosition > 1)
                PlaceSegment();
        }

        public void EndReached()
        {
            OnEndReached.Invoke();
        }
    }
}
