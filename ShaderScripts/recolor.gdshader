shader_type canvas_item;

uniform vec4 color : source_color = vec4(1);

void fragment() {
	vec4 spriteColor = texture(TEXTURE, UV);
	
	COLOR.rgb = mix(spriteColor.rgb, color.rgb, 0.6);
}
