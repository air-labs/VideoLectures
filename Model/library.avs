function AddWatermarkPNG(clip c, string filename, int x, int y) {
	length = c.frameCount
	logo = ImageSource(filename, end=length, pixel_type="rgb32")
	mask = logo.showAlpha()
	return overlay(c, logo, x, y, mask=mask)
}

function Intro(clip reference, float ms, string "image", string "title", string "description") {
	length = Time2Frame(reference, ms)
	fps = reference.frameRate
	logo = ImageSource(image, end=length-1, fps=fps, pixel_type="rgb24")  # no alpha!
	
	# set default values
	title = Default(title, "")
	description = Default(description, "")
	
	align = 5
	font = "Tahoma"
	size = 96
	text_color = $FFFFFF
	halo_color = $000000  # whats up with alpha channel?
	lsp = 1  # allow multiline; set multiline interval
	
	tmp1 = subtitle(logo, title, align=align,\
				font=font, size=size, lsp=lsp,\
				text_color=text_color, halo_color=halo_color)
	assert(tmp1.frameCount == length, "Invalid length #1 : "+string(tmp1.frameCount)+" != "+string(length))
	# redefine values for description
	align = 2
	font = "Tahoma"
	size = 36
	text_color = $FFFFFF
	halo_color = $00000088
	lsp = 1  # allow multiline; set multiline interval
	
	tmp2 = subtitle(tmp1, description, align=align,\
				font=font, size=size, lsp=lsp,\
				text_color=text_color, halo_color=halo_color)
	assert(tmp2.frameCount == length, "Invalid length #2 : "+string(tmp2.frameCount)+" != "+string(length))
	# add silent audio stream
	silence = KillVideo(BlankClip(audio_rate=44100, channels=2))
	tmp3 = AudioDub(tmp2, silence)
	tmp4 = Trim(tmp3, 0, length)
	assert(tmp4.frameCount == length, "Invalid length #3 : "+string(tmp4.frameCount)+" != "+string(length))
	return tmp4
}

function FadeInTime(clip c, float ms) {
	fadeIn(c, Time2Frame(c, ms))
}

function FadeOutTime(clip c, float ms) {
	fadeOut(c, Time2Frame(c, ms))
}

function CrossFadeTime(clip video, clip prev, float ms) {
	assert(round(prev.frameRate) == round(video.frameRate), "Clips must have same framerates: "+string(prev.frameRate)+" != "+string(video.frameRate))
	
	frameCount = Time2Frame(prev, ms)
	prevLastFrame = GetLastFrame(prev)
	prevLastLoop = loop(prevLastFrame, frameCount)
	
	mixedFragment = Dissolve(prevLastLoop, killAudio(video), frameCount)
	assert(mixedFragment.frameCount == video.frameCount, "Invalid mixed fragment length")
	
	mixedVideo = AudioDub(mixedFragment, KillVideo(video))
	assert(mixedVideo.frameCount == video.frameCount, "Invalid mixed video length")
	
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
