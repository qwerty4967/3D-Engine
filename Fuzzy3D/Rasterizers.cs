﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fuzzy3D
{
    public class BasicRasterizer : RasterizerModule
    {
        internal override object run()
        {
            
            // first of all, make the screen black:
            for (int x = 0; x < screenState.GetLength(0); x++)
            {
                for (int y = 0; y < screenState.GetLength(1); y++)
                {
                    
                    screenState[x, y] = Color.Black;
                    
                }
            }

            double slope = Fuzzy3D.activeCamera.FOV;
            
            for (int i = 0; i<LRS.Count; i++)
            {
                if(LRS[i] is FPolygon)
                {
                   
                    FPolygon poly = (FPolygon)LRS[i];
                    // now, we need to do the things!
                    // The HORROR!
                    // So, for each point in the poly:
                    for(int j = 0; j<poly.verticies.Length; j++)
                    {
                        // for each point, uhh...
                        /*Okay, so, let me lay out my understanding:
                        For each vertice in each polygon:
                        Determine the x value, and based on the cameras slope, get it’s 2D coords from the square defined by the slope,
                        Ie, one the upper left corner would be defined as, y=slopex, z=slopex, given that the viewport is square. LowerRight is the negative of both, and so on.
                        Take these coordinates and convert them into the full ( or small) sized coordinates of the viewport
                        This should be easy, actually:
			            I. for the x position: x(relative to the top left corner of the viewport)/width=realX/screenWidth
				        Just solve for realx and it’s done.
                        You now have the proper positioning of every vertice on the screen.
                        */

                        Vector3 vert = poly.verticies[j];
                        if(vert.X<=0)
                        {
                            // if it is behind the camera, it cannot be seen.
                            ((FPolygon)LRS[i]).screenVerticies = new Vector2[3] { new Vector2(-1, -1), new Vector2(-1, -1) , new Vector2(-1, -1)};
                            continue;
                        }
                        
                        // Now we find the size of the viewport.
                        // which requires the camera's FOV...
                        // Start by grabing the upper left corner:
                        // notice, we're implicitly implying that the viewport is square.
                        Vector2 viewportOrigin = new Vector2(-(float)slope*vert.X,-(float)slope*vert.X);
                        double viewportSize = (float)slope * vert.X * 2; // should be zero!
                      
                        Vector2 locationOnViewport = Vector2.Subtract(new Vector2(vert.Z, -vert.Y), viewportOrigin);
                      
                        
                        //okay, easy peasy, now just to get that to the screen
                        int x = (int)Math.Round((double)(locationOnViewport.X/viewportSize)*(screenState.GetLength(1)));
                        int y = (int)Math.Round((double)(locationOnViewport.Y / viewportSize) * (screenState.GetLength(1)));

                        ((FPolygon)LRS[i]).screenVerticies[j] = new Vector2(x, y);
                        if (x>=0&y>=0&x <screenState.GetLength(1) & y<screenState.GetLength(1))
                        {



                            
                            screenState[x, y] = Color.White;
                        }

                    }
                }
            }

            
            return base.run();
        }
    }
}
