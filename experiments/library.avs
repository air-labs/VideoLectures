function AddWatermarkPNG(clip c, string filename, int x, int y) {
	length = c.frameCount
	logo = ImageSource(filename, end=length, pixel_type="rgb32")
	mask = logo.showAlpha()
	return overlay(c, logo, x, y, mask=mask)
}

function Intro(clip reference, string filename, float ms, string "title", string "description") {
	length = Time2Frame(reference, ms)
	logo = ImageSource(filename, end=length, pixel_type="rgb24")  # no alpha!
	
	# set default values
	title = Default(title, "")
	description = Default(description, "")
	
	align = 5
	font = "Tahoma"
	size = 96
	text_color = $FFFFFF
	halo_color = $000000  # whats up with alpha channel?
	lsp = 1  # allow multiline; set multiline interval
	
	subtitle(logo, title, align=align,\
				font=font, size=size, lsp=lsp,\
				text_color=text_color, halo_color=halo_color)
	
	# redefine values for description
	align = 2
	font = "Tahoma"
	size = 36
	text_color = $FFFFFF
	halo_color = $00000088
	lsp = 1  # allow multiline; set multiline interval
	
	subtitle(description, align=align,\
				font=font, size=size, lsp=lsp,\
				text_color=text_color, halo_color=halo_color)
	
}

function FadeInTime(clip c, float ms) {
	fadeIn(c, Time2Frame(c, ms))
}

function FadeOutTime(clip c, float ms) {
	fadeOut(c, Time2Frame(c, ms))
}

function CrossFadeTime(clip a, clip b, float ms) {
	assert(round(a.frameRate) == round(b.frameRate), "Clips must have same framerates"+string(a.frameRate)+" / "+string(b.frameRate))
	
	frameCount = Time2Frame(a, ms)
	aLast = GetLastFrame(a)
	aLastLoop = loop(aLast, frameCount)
	
	mixedFragment = Dissolve(aLastLoop, killAudio(b), frameCount)
	assert(mixedFragment.frameCount == b.frameCount, "Invalid mixed fragment length")
	
	mixedVideo = a ++ AudioDub(mixedFragment, b)
	assert(mixedVideo.frameCount == a.frameCount + b.frameCount, "Invalid mixed video length")
	
	return mixedVideo
}


function GetLastFrame(clip c) {
	lastIndex = c.frameCount - 1
	lastFrame = Trim(c, lastIndex, 1)  # Trim() doesnt support negative start argument
	
	return killAudio(lastFrame)
}

/*
	Convert milliseconds to frames
	
	negative time specifies offset from the end of clip
	clip argument is essential to get framerate
	
	returns: int
*/
function Time2Frame(clip c, float ms) {
	f_ms = float(ms)  # well, that's a hack
	fps = c.frameRate  # float
	return round(f_ms/1000*fps)
}
