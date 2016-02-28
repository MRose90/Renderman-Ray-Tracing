

displacement ripple (
        float freq = 10;
        float scale = 1;
        point center = (0.,0.,0.);
        float speed = .1; )
    {
        N = -N;
        float currentFrame;
        option("Frame", currentFrame); 
        point Pcurrent = transform("world", P);  
        float max = currentFrame*speed;              
        float currentDist = length(Pcurrent - center); 
        float leadingEdge = (max-currentDist)*freq;
        P += normalize(N)*scale*(sin(leadingEdge)+1.);
        N = calculatenormal(P);  


    }

