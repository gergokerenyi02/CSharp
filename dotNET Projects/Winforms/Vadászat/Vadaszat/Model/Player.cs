using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Vadaszat;
using System.Drawing;

namespace Vadaszat
{
    
    public abstract class Player
    {
        #region Fields

        public Player? _player1 { get; private set; }
        public Player? _player2 { get; private set; }

        #endregion


        #region Public methods


        public static Player? createPlayer(int n)
        {

            
            switch (n)
            {
                case 1:

                    return new Runner();

                case 2:
                    return new Attacker();

                default:
                    return null;
            }
        }
        #endregion
    }

    public class Attacker : Player
    {

        #region Fields

        private Point[] positions;
        public int setPositions {  get; private set; }

        #endregion

        #region Constructor


        public Attacker()
        {
            positions = new Point[4];
            setPositions = 0;
        }
        #endregion

        #region Public methods

        public void ResetSetPositions()
        {
            setPositions = 0;
        }
        public void SetPlayer(Point p)
        {
            positions[setPositions] = p;
            setPositions++;
        }

        /*
         * DEBUG
        public string Locations()
        {
           string positionString = "";

           for (int i = 0; i < positions.Length; i++)
           {
               positionString += $"{positions[i].ToString()}\n";
           }
           return positionString;
           
        }
        */
        public Point Location(int n)
        {
            return positions[n];
           
        }

        public void Move(Point newLocation, int selectedCharacter)
        {
            positions[selectedCharacter] = newLocation;
        }


        #endregion

    }



    public class Runner : Player
    {
        #region Fields

        private Point playerPositon;
        #endregion

        #region Constructor
        public Runner()
        {

        }

        #endregion

        #region Public methods


        public void SetPlayer(Point p)
        {
            playerPositon = p;
        }

        public Point Location()
        {
            return this.playerPositon;
        }


        public void Move(Point newLocation)
        {
            playerPositon = newLocation;

        }
        #endregion
    }



}
