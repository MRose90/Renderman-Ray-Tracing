surface
bottom (
        float Ka = 1;
        float Ks = 1;
        float Kr = 1;
        float roughness = .1;
        color color1 = color( 1., 0.0784, 0.577 );
		color color2 = color( 0.577, 0.44, 0.86 );
        string texturename = ""; )
    {
        normal Nf = faceforward (N, I);
        vector V = -normalize (I);
        vector D = reflect (I, normalize (Nf));
        color TheColor = (0.2,0.2,0.2);
        color c;
        Ci =  TheColor;  
		gather("illuminance", P, Nf,0,2, "surface:Ci", c) {
			Ci = (1.,1.,1.)-c;
		}      
    }