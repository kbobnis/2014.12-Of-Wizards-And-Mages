using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Assets.Resources.Scripts.AI
{
    class Node
    {
        List<Node> Adj;
        int size;
        Vector3 left, rigth;
        bool free = true;

        public Node(int size, Vector3 left)
        {
            this.size = size;
            this.left = left;
            Adj = new List<Node>();
            rigth = new Vector3(left.x + size, left.y, left.z); // left.x this.left + size;
        }

        public bool isFree()
        {
            return free;
        }
        public void addTarget(Node target) {
            Adj.Add(target);
        }

        public void placeBullet()
        {
            free = false;
        }
        public void clear()
        {
            Adj.Clear();
            free = true;
        }
    }
}
