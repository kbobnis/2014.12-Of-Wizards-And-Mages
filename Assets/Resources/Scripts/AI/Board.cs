using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Resources.Scripts.AI
{
    public enum Direction { 
        LEFT,
        RIGHT,
        NOt_PATH
    }
    public class Board
    {
        
        private Vector3 MageLeftBottom, MageRightTop, BoardLeftBottom, BoardRightTop, BulletOneLeftTop, BulletOneRightBottom, BulletTwoLeftTop, BulletTwoRightBottom;

        Vector3[] magslice;
        private float sliceWidth;
        private float sliceHeight;
        
        //TODO fit those params after path searching!!!!
        private int chunkWidth = -1;
        private int sliceArraySize = -1;

        private  const int SLICE_SIZE = 100;
        private  const float SLICE_TIME = 0.080f;


        Node[][] grid;
        public int currentSlice = 0;
        //(min, max)
        int[] magChunks = new int[2];


        public float currentFameTime = 0.0f;
        public float lastFameChangeTime = 0.0f;


        Vector3[] corners;
        Vector3[] topCorners;
        Vector3[] magCorners;
        GameObject magGameObject;

        public Board(Collider mage, RectTransform panelMinigame, RectTransform bulletOne, RectTransform bulletTwo)
        {
            magGameObject = mage.gameObject;

            GameObject mainGame = GameObject.FindWithTag("MainGame");
            RectTransform mainTr = mainGame.GetComponent<RectTransform>();
            corners = new Vector3[4];
            mainTr.GetWorldCorners(corners);

            GameObject top = GameObject.FindWithTag("PanelTop");
            RectTransform topPanel = top.GetComponent<RectTransform>();
            topCorners = new Vector3[4];
            topPanel.GetWorldCorners(topCorners);

            updateMagPosition();


            

            magslice = new Vector3[4];

            magslice[0] = new Vector3(topCorners[0].x, magCorners[0].y, magCorners[0].z);
            magslice[1] = new Vector3(topCorners[0].x, magCorners[1].y, magCorners[0].z);

            magslice[2] = new Vector3(topCorners[2].x, magCorners[0].y, magCorners[0].z);
            magslice[3] = new Vector3(topCorners[2].x, magCorners[1].y, magCorners[0].z);

            sliceWidth = Math.Abs(magslice[1].x - magslice[2].x);
            sliceHeight = Math.Abs(magslice[1].y - magslice[0].y);

            float magJump = 150 * 0.08f;
            chunkWidth = (int)(magJump + 0.5);
            sliceArraySize = (int)((Math.Abs(magslice[0].x - magslice[3].x) / chunkWidth) + 0.5);




            magChunks = calculateChunks(magCorners[0].x, magCorners[3].x);


            generateGrid(sliceArraySize, magJump);

            Debug.Log("kokos");
        }

        private bool checkCross(int left, int right, int currentTraverseSlice)
        {
            for (int i = left; i <= right; i++)
            {
                if(i < sliceArraySize && !grid[currentTraverseSlice][i].isFree()){
                    return false;
                }
            }
            return true;
        }

        private Direction goLeft(int currentTraverseSlice, float leftMagePosition, int depth, int[] postitions, float magJump)
        {
            float newRightMagePosition = leftMagePosition - magJump;
            int[] newPosition = calculateChunks(newRightMagePosition, newRightMagePosition + (int)(Math.Abs(magCorners[0].x - magCorners[3].x) + 0.5));
            //check left bucket
            if (newPosition[1] < sliceArraySize && newPosition[0] >= 0 && checkCross(newPosition[0], newPosition[1], currentTraverseSlice) ) //grid[currentTraverseSlice][newPosition[0]].isFree())
            {
                if (depth == 10)
                {
                    return Direction.LEFT;
                }
                Direction res = traverseGrid(currentTraverseSlice, depth + 1, newRightMagePosition, false, newPosition, Direction.LEFT);

                if (res != Direction.NOt_PATH)
                {
                    return res;
                }
            }
            return Direction.NOt_PATH;
        }

        private Direction goRight(int currentTraverseSlice, float leftMagePosition, int depth, int[] postitions, float magJump)
        {
            //goRight
            float newLeftMagePosition = leftMagePosition + magJump;
            int[] newPosition = calculateChunks(newLeftMagePosition, newLeftMagePosition + (int)(Math.Abs(magCorners[0].x - magCorners[3].x) + 0.5));
            //check left bucket
            if (newPosition[1] <= sliceArraySize && checkCross(newPosition[0], newPosition[1], currentTraverseSlice)) //grid[currentTraverseSlice][newPosition[0]].isFree())
            {
                if (depth == 10)
                {
                    return Direction.RIGHT;
                }
                Direction res = traverseGrid(currentTraverseSlice, depth + 1, newLeftMagePosition, false, newPosition, Direction.RIGHT);
                if (res != Direction.NOt_PATH)
                {
                    return res;
                }
            }
            return Direction.NOt_PATH;
        }
        private Direction traverseGrid(int currentTraverseSlice, int depth, float leftMagePosition, bool right, int[] postitions, Direction prevDirection)
        {

                currentTraverseSlice = (currentTraverseSlice + 1) % SLICE_SIZE;//(postitions[0] + 1) % SLICE_SIZE;

                //float magJump = 150 * 0.08f;
                float magJump = 150 * Time.deltaTime;

                if (prevDirection == Direction.LEFT)
                {
                    //goLeft
                    Direction leftDirection = goLeft(currentTraverseSlice, leftMagePosition, depth, postitions, magJump);
                    if (leftDirection != Direction.NOt_PATH)
                    {
                        return leftDirection;
                    }
                    //goRight
                    Direction rightDirection = goRight(currentTraverseSlice, leftMagePosition, depth, postitions, magJump);
                    if (rightDirection != Direction.NOt_PATH)
                    {
                        return rightDirection;
                    }
                }
                else
                {

                    //goRight
                    Direction rightDirection = goRight(currentTraverseSlice, leftMagePosition, depth, postitions, magJump);
                    if (rightDirection != Direction.NOt_PATH)
                    {
                        return rightDirection;
                    }

                    //goLeft
                    Direction leftDirection = goLeft(currentTraverseSlice, leftMagePosition, depth, postitions, magJump);
                    if (leftDirection != Direction.NOt_PATH)
                    {
                        return leftDirection;
                    }
                }


                return Direction.NOt_PATH;
        }

        private int[] calculateChunks(float magLeft, float magRight)
        {
            int[] toReturn = new int[2];
            toReturn[0] = (int)((magLeft - topCorners[0].x) + 0.5) / chunkWidth;
            if(toReturn[0] <= 0 ){
                toReturn[0] = 0;
            }
            toReturn[1] = (int)((magRight - topCorners[1].x) + 0.5) / chunkWidth;
            return toReturn;
        }
        // TODO: Refactor me:
        //loops
        //if branches it looks ugly now
        public void generateGrid(int sliceArraySize, float magJump)
        {
            grid = new Node[SLICE_SIZE][];
            for (int i = 0; i < SLICE_SIZE; i++)
            {
                grid[i] = new Node[sliceArraySize];
                for (int j = 0; j < sliceArraySize; j++)
                {
                    Vector3 left = new Vector3(magslice[0].x + chunkWidth, magslice[0].y, magslice[0].z);
                    grid[i][j] = new Node(chunkWidth, left);
                }
            }
        }

        private void updateMagPosition()
        {
            GameObject mag = GameObject.FindWithTag("MagImage");
            RectTransform magPanel = mag.GetComponent<RectTransform>();
            magCorners = new Vector3[4];
            magPanel.GetWorldCorners(magCorners);
        }

        public void GameUpdate() {
           
            int[] oldChunks = new int[2] { magChunks[0], magChunks[1]};

            Direction direction = traverseGrid(currentSlice, 0, magCorners[0].x, RightMove, oldChunks, RightMove ? Direction.RIGHT : Direction.LEFT);
            if (Direction.NOt_PATH == direction)
            {
                Debug.Log("Bad path: " + direction);
            }
            RightMove = direction == Direction.RIGHT ? true : false;

            float distance = moveMag();

            updateMagPosition();
            magChunks = calculateChunks( magCorners[0].x, magCorners[3].x );
          


            currentFameTime += Time.deltaTime;

            float diff = currentFameTime - lastFameChangeTime;

            if (diff > SLICE_TIME)
            {
                if (RightMove)
                {
                    if (oldChunks[1] != sliceArraySize)
                    {
                        oldChunks[0]++;
                        oldChunks[1]++;
                    }
                }
                else
                {
                    if (oldChunks[0] != 0)
                    {
                        oldChunks[0]--;
                        oldChunks[1]--;
                    }
                }
                String logChunks = "(" + oldChunks[0] + ", " + oldChunks[1] + " ), ( " + magChunks[0] + ", " + magChunks[1] + ")"; 
                lastFameChangeTime = currentFameTime;
                int oldSliceId = currentSlice++;
                for (int i = 0; i < sliceArraySize; i++)
                {
                    grid[oldSliceId][i].clear();
                }

                if (currentSlice == SLICE_SIZE) {
                    currentSlice = 0;
                }
               // Debug.Log("Change slice" + logChunks);
            }
            else
            {
              //  Debug.Log("No change slice: " + diff );
            }

        }

        public bool RightMove = false;
        public float moveMag()
        {

            RectTransform tr = magGameObject.GetComponent<RectTransform>();
            SphereCollider col = magGameObject.GetComponent<SphereCollider>();
            Vector3 oldPos = magGameObject.GetComponent<RectTransform>().position;

            float w = magGameObject.GetComponent<RectTransform>().GetWidth();
            float distance = (float)150 * (RightMove ? 1 : -1) * Time.deltaTime;
            float xPos = oldPos.x + distance;
            //float xPos = oldPos.x + (float)3 * (RightMove ? 1 : -1) ;
            float x = Game.GetXPositionFromGlobal(xPos + w / 2 * (RightMove ? 1 : -1));
            if (x > 1)
            {
                RightMove = false;
            }
            if (x < -0)
            {
                RightMove = true;
            }

            magGameObject.GetComponent<RectTransform>().position = new Vector3(xPos, oldPos.y, oldPos.z);
            return distance;
        }


        private int[] getBulletHitBuckets(float bulletCornerY, float buletCornerX, Vector3 direction, float bulletWidth, float magSliceY)
        {

            float yDistance = Math.Abs(bulletCornerY - magSliceY);
            float time = yDistance / (direction.y * 5);
            float xDistance = time * direction.x * 5;
            int buckets = (int)(Math.Abs(time / SLICE_TIME) + 0.5) ;

            int[] ceilFloor = calculateChunks(buletCornerX + xDistance, buletCornerX + xDistance + bulletWidth);
            return new int[3] { ceilFloor[0], ceilFloor[1], buckets };//ceilFloor;
        }
        public void addBullet(Vector2 from, Vector3[] bulletCorners, Vector3 direction){
            //magslice
            float bulletWidth = (int)(Math.Abs(bulletCorners[0].x - bulletCorners[3].x) + 0.5);

          //  float yDistance = Math.Abs(bulletCorners[1].y - magslice[0].y);
          //  float time = yDistance / direction.y;
         //   float xDistance = time * direction.x;
         //   int buckets = (int)(Math.Abs(time / SLICE_TIME) + 0.5);

         //   int[] ceilFloor = calculateChunks(bulletCorners[0].x + xDistance, bulletCorners[0].x + xDistance + bulletWidth);
            int[] ceilFloor = getBulletHitBuckets(bulletCorners[1].y, bulletCorners[0].x, direction, bulletWidth, magslice[0].y);
            int[] ceilCeil = getBulletHitBuckets(bulletCorners[1].y, bulletCorners[0].x, direction, bulletWidth, magslice[1].y);

            int xFrom = Math.Min(ceilFloor[0], ceilCeil[0]);
            int xTo = Math.Max(ceilFloor[1], ceilCeil[1]);
            int timeFrom = Math.Min(ceilFloor[2], ceilCeil[2]);
            int timeTo = Math.Max(ceilFloor[2], ceilCeil[2]);

         //   Debug.Log("xFrom: " + xFrom + ", xTo: " + xTo + ", timeFrom: " + ((currentSlice + timeFrom) % SLICE_SIZE) + ", timeTo: " + ((currentSlice + timeTo) % SLICE_SIZE));
            
                for (int i = xFrom; i <= xTo; i++)
                {
                    for (int j = timeFrom; j <= timeTo; j++)
                    {
                        int timeId = (currentSlice + j) % SLICE_SIZE;
                        grid[timeId][i].placeBullet();
                    }
                }


            int[] floorFloor = getBulletHitBuckets(bulletCorners[0].y, bulletCorners[0].x, direction, bulletWidth, magslice[0].y);
            int[] floorCeil = getBulletHitBuckets(bulletCorners[0].y, bulletCorners[0].x, direction, bulletWidth, magslice[1].y);

            xFrom = Math.Min(floorFloor[0], floorCeil[0]);
            xTo = Math.Max(floorFloor[1], floorCeil[1]);
            timeFrom = Math.Min(floorFloor[2], floorCeil[2]);
            timeTo = Math.Max(floorFloor[2], floorCeil[2]);

            for (int i = xFrom; i <= xTo; i++)
            {
                for (int j = timeFrom; j <= timeTo; j++)
                {
                    int timeId = (currentSlice + j) % SLICE_SIZE;
                    grid[timeId][i].placeBullet();
                }
            }
           // Debug.Log("xFrom: " + xFrom + ", xTo: " + xTo + ", timeFrom: " + timeFrom + ", timeTo: " + timeTo);

        }     

    }



    

}
