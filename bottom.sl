surface
bottom (
        string texturename = ""; )
    {
        //calculates the normal
        normal Nf = faceforward (N, I);
        vector V = -normalize (I);
        color c = (0.,0.,0.);
        Ci =  (0.2,0.2,0.2); 
        float angle = 0.;
        float samples = 1.;
        //uses gather to get information based upon the normal. 
        gather("illuminance", P, Nf,angle,samples, "surface:Ci", c) {
            Ci = (1.,1.,1.)-c;
        }      
    }