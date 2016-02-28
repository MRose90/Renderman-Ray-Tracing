using System;

namespace RenderManAnimation
{
    class Animation
    {
        static void Main(string[] args)
        {
            int x = 1920;
            int y = 1080;
            int FPS = 24;
            int time = 5;
            int rotations = 1;
            int angle;
            double rippleScale = .1;
            double freq = 10.0;
            double bounce;
            int frameCount = FPS*time+1;
            string[] frames = new string[frameCount];
            //saves space
            string nl = Environment.NewLine;
            frames[0] = "##RenderMan RIB"+nl;
            for (var i = 1; i < frameCount; i++)
            {
                //change 0 to comment to make it bounce
                bounce = 0;//Math.Abs(3 * (Math.Cos(i*FPS * time * 20)-1.0));
                angle = i* rotations*360 / (FPS*time);
                int temp = frameCount.ToString("D").Length;
                frames[i] = "FrameBegin " + (i)+nl;
                frames[i] += "Format " + x + " " + y + " -1" + nl;
                frames[i] += "Display \"" + (i).ToString("D" + temp.ToString()) + ".tiff\" \"tiff\" \"rgb\"" + nl;
                frames[i] += "PixelSamples 3 3" + nl + "ShadingRate 1" + nl + "Declare \"freq\"  \"uniform float\"" + nl;
                frames[i] += "Declare \"Ad\"  \"uniform float\"" + nl + "Declare \"Bd\"  \"uniform float\"" + nl + "Declare \"Amp\"  \"uniform float\"" + nl;
                frames[i] += "Declare \"scale\" \"uniform float\"" + nl + "Projection \"perspective\" \"fov\" 25" + nl + "Translate 0 0 25" + nl;
                frames[i] += "Rotate -45 1 -1 0" + nl + "WorldBegin"+nl+ "Attribute \"bound\" \"displacement\" [1.5]" + "LightSource \"ambientlight\" 1 \"intensity\"[0.25]" + nl;
                frames[i] += "LightSource \"shadowspot\" 2 \"from\"[0 10 10] \"intensity\"[12] \"string shadowname\"[\"raytrace\"]" + nl + "Attribute \"visibility\" \"int diffuse\"[1]" + nl;
                frames[i] += "Attribute \"visibility\" \"int specular\"[1]" + nl + "Attribute \"visibility\" \"int transmission\"[0]" + nl + "Attribute \"trace\" \"int maxspeculardepth\"[2]" + nl;

                frames[i] += nl+"AttributeBegin" + nl + "Attribute \"identifier\" \"name\" [\"bottom\"]" + nl + "Rotate 110 1 0 0" + nl + "Translate 0 1 3" + nl;
                frames[i] += "Surface \"bottom\" \"Ad\" 0.05 \"Bd\" 0.05 \"Amp\" .75 \"texturename\" [\"raytrace\"]" + nl;
                frames[i] += "Disk .2 5. 360." + nl + "AttributeEnd" + nl + nl;

                frames[i] += nl + "AttributeBegin" + nl + "Attribute \"identifier\" \"name\" [\"back\"]" + nl + "Translate 1 1 3" + nl + "Rotate -30 1 -1 0" + nl;
                frames[i] += "Color [1 0 0]"+nl+ "Surface \"back\" \"texturename\" [\"raytrace\"]" + nl;
                frames[i] += "Disk .2 5. 360." + nl + "AttributeEnd" + nl + nl;

                frames[i] += nl + "AttributeBegin" + nl + "Attribute \"identifier\" \"name\" [\"front\"]" + nl + "Rotate 80 .25 1 0"+ nl + "Translate 1 0 -5" + nl;
                frames[i] += "Color [1 0 0]" + nl + "Surface \"front\" \"texturename\" [\"raytrace\"]" + nl;
                frames[i] += "Disk .2 5. 360." + nl + "AttributeEnd" + nl + nl;

                frames[i] += "AttributeBegin"  + nl + "Attribute \"identifier\" \"name\"[\"deer\"]"+nl + "Translate 0 " + bounce + " 0"+nl + "Rotate " + angle + " 0 1 0"+nl;
                frames[i] += "Color[1. 1. 1.]" + nl + "Surface \"shinymetal\" \"texturename\" [\"raytrace\"]" + nl+"Displacement \"ripple\" \"freq\" "+freq+" \"scale\" "+rippleScale + nl + "ReadArchive \"deer.rib\"" + nl;
                frames[i] += "AttributeEnd" + nl;

                frames[i] += nl + "WorldEnd" + nl + "FrameEnd" + nl+nl+nl;
            }
            System.IO.File.WriteAllLines(@"C:\Renderman\Test\render.rib", frames);
        }
    }
}










