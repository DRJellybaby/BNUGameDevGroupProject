Shader "Custom/HologramShader"
{
	Properties{
	 _RimColor("Rim Color", Color) = (0,0,0,0.0) //colour picker that determines the colour of the edges.
	 _RimPower("Rim Power", Range(0.5, 8.0)) = 3.0 //defines the strength of the outer rim, within arrange of 0.5 to 8 (default 3). the higher the value the less apparent the edge lighting will be
	}
		SubShader{

			   Tags {"Queue" = "Transparent"} //sets the render queue to "optimise" for transparent. Ef-fetely making the object transparent. 

			   Pass {
				   ZWrite on //Stops the system from rendering sections of the object that are be-hind other sections. when not in the Transparent queue this would cause objects behind to not be rendered
				   ColorMask 0 //removes all channels (RGBA), essentially removing the actual object.
			   }

			 CGPROGRAM //Encloses CG code block
			 #pragma surface surf Lambert alpha:fade 
			//"surface surf" sets the struct for the surf function, as a surface. 
			//"Lambert" sets the lighting model to Diffuse
			//"alpha:fade" Enable traditional fade-transparency.
			struct Input
			{
				float3 viewDir; //gets the direction the model is viewed from. 
			};

			  float4 _RimColor; //float for the RGBA values passed from the property of the same name
			  float _RimPower; //float that gets current value of the property of the same name

			//the surf function defined earlier. 
			//"Input IN" gets the details from the input struct, in this case the view direction.
			//"SurfaceOutput o" defines the surface (Emission and alpha in this case). "o" is a naming convention
			void surf(Input IN, inout SurfaceOutput o)
			{
				   half rim = 1 - saturate(dot(normalize(IN.viewDir), o.Normal));
				   //above sets the edge lighting. the first number determines the base degree to which the lighting is effected. 
				   //"saturate"  fixes the value to be between 0 and 1.
				   //"dot" returns a single value between two vectors that determines their relation-ship (angle).
				   //"normalize" sets the viewDir in the Input struct to 1, while not changing the direction.
				   o.Emission = _RimColor.rgb * pow(rim, _RimPower) * 10; //sets the Emission (shine) colour to be equal to the _RimColor Property/Variable, excluding the alpha value. then sets the strength of the shine 
				   o.Alpha = pow(rim, _RimPower); //sets the alpha to the rim variable to the power of the _RimPower Property/Variable
			  }
		  ENDCG //Encloses CG code block
	}
		FallBack "Diffuse" //sets the shader to diffuse if no subshader is found. 
}

