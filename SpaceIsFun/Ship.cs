﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceIsFun
{
    class Weapon : Entity
    {
        private bool readyToFire;
        public bool ReadyToFire
        {
            get
            {
                return readyToFire;
            }

            set
            {
                readyToFire = value;
            }
        }

        private bool aimedAtTarget;

        public bool AimedAtTarget
        {
            get
            {
                return aimedAtTarget;
            }

            set
            {
                aimedAtTarget = value;
            }
        }


    }
    class Grid : Entity
    {
        #region fields
        /// <summary>
        /// x,y position of the grid on the ship (ie, top left grid will be 1,1)
        /// </summary>
        private Vector2 gridPosition;

        public Vector2 GridPosition
        {
            get
            {
                return gridPosition;
            }

            set
            {
                gridPosition = value;
            }
        }

        /// <summary>
        /// drawable for the grid (this is probably never gonna move)
        /// </summary>
        private Drawable sprite;

        public Drawable Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        private bool highlighted;

        public bool Highlighted
        {
            get
            {
                return highlighted;
            }

            set
            {
                highlighted = value;
            }
        }

        private Texture2D gridTexture;

        private Texture2D gridHighlightTexture;

        public Texture2D GridTexture
        {
            get
            {
                return gridTexture;
            }

            set
            {
                gridTexture = value;
            }
        }

        public Texture2D GridHighlightTexture
        {
            get
            {
                return gridHighlightTexture;
            }

            set
            {
                gridHighlightTexture = value;
            }
        }


        #endregion

        #region constructor / destructor
        public Grid()
            : base()
        {
        }

        public Grid(Texture2D spriteTexture, Texture2D highlightTexture, Vector2 position, Vector2 gPosition) 
            : base()
        {
            gridTexture = spriteTexture;
            gridHighlightTexture = highlightTexture;
            sprite = new Drawable(gridTexture, position);
            gridPosition = gPosition;
        }

        #endregion

        #region methods

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (highlighted == true)
            {
                Sprite.SpriteTexture = gridHighlightTexture;
            }

            if (highlighted == false)
            {
                Sprite.SpriteTexture = gridTexture;
            }

            Sprite.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public void Highlight()
        {
            if (highlighted == true)
            {
                highlighted = false;
            }

            else
            {
                highlighted = true;
            }
        }
        #endregion




    }
    /// <summary>
    /// This contains all the info of a room on a ship 
    /// </summary>
    class Room : Entity
    {
        /// <summary>
        /// grid position of the room's top-left grid
        /// </summary>
        private Vector2 roomPosition;

        public Vector2 RoomPosition
        {
            get
            {
                return roomPosition;
            }

            set
            {
                RoomPosition = value;
            }
        }

        public Room()
        {

        }

        
    }


    class Ship : Entity
    {
        #region fields
        private int hp;
        private int shields;
        private int energy;
        private Grid[,] shipGrid;
        private Texture2D gridTexture;

        public int HP
        {
            get
            {
                return hp;
            }

            set
            {
                hp = value;
            }
        }

        public int Shields
        {
            get
            {
                return shields;
            }
            set
            {
                shields = value;
            }
        }

        public int Energy
        {
            get
            {
                return energy;
            }
            set
            {
                energy = value;
            }
        }

        private Drawable sprite;

        public Drawable Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        public Grid[,] ShipGrid
        {
            get
            {
                return shipGrid;
            }

            set
            {
                shipGrid = value;
            }
        }

        #endregion

        #region constructors / destructors

        public Ship(Texture2D shipTexture, Texture2D gridTexture, Texture2D highlightTexture, Vector2 position)
            : base()
        {
            hp = 10;
            energy = 5;
            shields = 2;

            sprite = new Drawable(shipTexture, position);
            int gridWidth = shipTexture.Bounds.Width / 32;
            int gridHeight = shipTexture.Bounds.Height / 32;
            shipGrid = new Grid[gridWidth, gridHeight];

            for (int i = 0; i < shipTexture.Bounds.Width/32; i++)
            {
                for (int j = 0; j < shipTexture.Bounds.Height/32; j++)
                {
                    shipGrid[i,j] = new Grid(gridTexture, highlightTexture, new Vector2(i*32+position.X, j*32+position.Y), new Vector2(i,j));
                    
                }
            }

        }

        #endregion

        #region methods

        override public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
            foreach (Grid shipgrid in shipGrid)
            {
                shipgrid.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }

        public bool checkShipHover(MouseState currentMouseState)
        {
            if (((currentMouseState.X > sprite.Position2D.X)
                    && (currentMouseState.X < sprite.Position2D.X + sprite.Width)
                  && ((currentMouseState.Y > sprite.Position2D.Y)
                    && (currentMouseState.Y < sprite.Position2D.Y + sprite.Height))))
             {
                // our mouse cursor should be within the bounds of the ship
                //System.Diagnostics.Debug.WriteLine("Cursor on the ship!");
                return true;
             }

            else
            {
                return false;
            }

        }

        public Vector2 checkGridHover(MouseState currentMouseState)
        {
            // we know the cursor is within bounds, this will only get called if checkShipHover returns true

            Vector2 ret = new Vector2();

            // x position relative to the ship
            float relativeXPos = currentMouseState.X - sprite.Position2D.X;
            // y position relative to the ship
            float relativeYPos = currentMouseState.Y - sprite.Position2D.Y;

            ret.X = (int)relativeXPos / 32;
            ret.Y = (int)relativeYPos / 32;

            

            return ret;
        }

        #endregion 
    }
}
