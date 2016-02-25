surface
back (
        float Ka = 1;
        float Ks = 1;
        float Kr = 1;
        float Ad = 0.025;				// width of each stripe
		float Bd = 0.10;				// height of each stripe
		float Amp = 0;
        float roughness = .1;
        color color1 = color( 1., 0.0784, 0.577 );
		color color2 = color( 0.423, 0.56, 0.14 );
        string texturename = ""; )
{
	// be sure the normal points correctly (used for lighting):

	varying vector Nf = faceforward( normalize( N ), I );
	vector V = normalize( -I );

	// determine how many squares over and up we are in right now:

	float up = 2. * u;	// because we are rendering a sphere
	float vp = v;
	float numinu = floor( up / (2*Ad) );
	float numinv = floor( vp / (2*Bd) );
	float uc = numinu * 2 * Ad + Ad;
	float vc = numinv * 2 * Bd + Bd;
	
	// use whatever opacity the rib file gave us)

	Oi = Os;

	// Noise
  point PP = point "shader" P;
	float magnitude = 0.;
	float size = 2.;
	float i;
	for( i = 0.; i < 7.0; i += 1.0 )
	{
		magnitude += ( noise(size * PP ) - 0.50 ) / size;
		size *= 2.;
	}

	point upvp =  point( up, vp, 0. );      // the point
	point cntr =  point( 0., 0., 0. );      // the center
	vector delta = upvp-cntr;			// vector from center to u',v'
	float oldrad = length(delta);		// result from the ellipse equation
	float newrad = oldrad+Amp*magnitude;
	delta = delta * newrad / oldrad;
	float deltau = xcomp(delta);
	float deltav = ycomp(delta);
	color TheColor = color1;
	float newnuminu = floor( deltau / (2*Ad) );
	float newnuminv = floor( deltav / (2*Bd) );
	float u = newnuminu * 2 * Ad + Ad;
	float v = newnuminv * 2 * Bd + Bd;
	float d = ((deltau-u)/Ad)*((deltau-u)/Ad)+((deltav-v)/Bd)*((deltav-v)/Bd);
	if( d <= 1. )
	{
		TheColor = color2;
	}
	// determine the lighted output color Ci:

       color c;
      Ci =  TheColor;  
		gather("illuminance", P, Nf,0,2, "surface:Ci", c) {
			Ci = c-TheColor;
		}   
}