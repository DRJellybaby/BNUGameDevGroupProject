Shader "Custom/BloomShader"
{
	Properties
	{
		_Colour("Colour", Color) = (1,1,1,1) //sets the RGB values and alpha.
		_Size("Bloom Size", Range(0,500)) = 4 //sets the size of the bloom effect.
		_Fade("Fade Power", Range(0,100)) = 4 //sets the drop-off for the glow effect.
	}
		SubShader{


			CGPROGRAM //first pass, allows the colour picker for the base object effect. The shader is done in two blocks in order to duplicate the base object and apply different effects to the different instances of the object 
			#pragma surface surf Lambert
			//"surface surf" sets the struct for the surf function, as a surface. 
			//"Lambert" sets the lighting model to Diffuse
			struct Input
			{
				float4 trash; //holds void data to stop erroring due to empty input struct
			};

			float4 _Colour;//variable to use the output of the properties of the same name as a float

			void surf(Input IN, inout SurfaceOutput o) //defines the surface of the object (the base object)
			{

				o.Albedo = _Colour.rgb; //sets the Albedo of the texture to the RGB value of _Colour (first 3 digits)
				o.Alpha = _Colour.a; //sets the alpha to the 4th digit of _Colour
			}
			ENDCG

			Cull Front //stops rendering polygons facing the camera. essentially only rendering the in-ternal of a shape

			CGPROGRAM//second pass, creates a larger object, reduces it into a bloom, and makes it match the colour of the first pass via the same colour picker. being another CGPROGRAM block stops the two effects code interacting with each other
			#pragma surface surf Lambert fullforwardshadows alpha:fade //sets up a new surf function as a surface. sets the lighting model as diffuse (lambert) 
			#pragma vertex vert//defines a new vertex struct

			// Use shader model 3.0 target, to get nicer looking lighting. (Equivalent to DirectX shader model 3.0)
			#pragma target 3.0


			struct Input
			{
				float3 viewDir; //gets the direction the object is being viewed from (the camera)
			};

			half _Size; //variable to use the output of the properties of the same name
			half _Fade; //variable to use the output of the properties of the same name
			fixed4 _Colour; //variable to use the output of the properties of the same name (RGBA)

			void vert(inout appdata_full v)//defines the vertex to modify the objects geometry
			{
				v.vertex.xz += v.vertex.xz * _Size / 5;//rescales the original object and breaks it down to a glow effect
				v.vertex.y += v.vertex.y * _Size / 100;
				v.normal *= -1; //inverts normal, making the glow effect visible from the inside of the object. using "cull front" this is how the objects glow doesn’t overlap the base object while retaining the outer glow
			}

			void surf(Input IN, inout SurfaceOutput o) //defines the surface of the object (the scaled, faded object)
			{
				half rim = saturate(dot(normalize(IN.viewDir), normalize(o.Normal))); //gets the dot product of the direction the object is viewed from and its normal, then sets that to be a value between 0 and 1
				o.Albedo = _Colour.rgb; //sets the Albedo of the texture to the RGB value of _Colour (first 3 digits)
				o.Alpha = _Colour.a; //sets the alpha for the objects surface, to the 4th digit of _Colour. setting the base transparency of the second object to be something below 100%
				o.Emission = _Colour.rgb; //sets the Emission for the objects surface to the first 3 digits of _Colour.
				o.Alpha = lerp(0, 1, pow(rim, _Fade)); //sets the alpha value of the surface to be interpolation of the rim, and the fade. essentially gradually reducing the alpha value further, the further out it is.	
			}
			ENDCG
	}
		FallBack "Diffuse" //sets the shader to diffuse if no subshader is found.
}

