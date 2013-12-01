@setlocal
@echo off
PATH C:\ffmpeg\bin\

del tmp__*
del test*.mp4

rem call :fadein sample.avi 3 fadein.avi
call :crossfade_frame sample.avi sample.mp4 5 3 test_cross.mp4

GOTO:EOF
endlocal

:fadein
	rem Apply fade-in effect on given input
	rem 
	rem Args:
	rem * input filename
	rem * duration of an effect
	rem * output filename
	set input=%~1
	set duration=%~2
	set output=%~3
	rem background color
	rem ffmpeg's default is 'black'
	set color=black
	ffmpeg -i %input% -vf fade=in:st=0:d=%duration%:color=%color% %output%
	GOTO:EOF


:fadeout
	rem Apply fade-out effect on given input
	rem
	rem NOTE: Values of length and duration may be inaccurate;
	rem       Filter just fades into background color until the end of an input.
	rem
	rem Args:
	rem * input filename
	rem * input length
	rem * duration of an effect
	rem * output filename
	set input=%~1
	set video_length=%~2
	set duration=%~3
	set output=%~4
	rem background color
	rem ffmpeg's default is 'black'
	set color=black
	rem calculate offset to start effect
	rem start = video length - effect duration
	set /A start=%video_length%-%duration%
	ffmpeg -i %input% -vf fade=out:st=%start%:d=%duration%:color=%color% %output%
	GOTO:EOF


:crossfade_frame
	rem Cross-fade last frame of input1 into input2
	rem 
	rem NOTE: Result is the concatenation of both inputs;
	rem       Duration of videos is preserved.
	rem
	rem Args:
	rem * input1 filename
	rem * input2 filename
	rem * input1 length (for faster last frame capturing)
	rem * duration of an effect
	rem * output filename
	set input1=%~1
	set input2=%~2
	set input1_len=%~3
	set duration=%~4
	set output=%~5
	set tmp_frame=tmp__frame.png
	REM this preserves extension to auto-select output format
	set tmp_video=tmp__%output%
	
	call :get_last_frame %input1% %input1_len% %tmp_frame%
	call :image_to_video %tmp_frame% %duration% %tmp_video% 
	GOTO:EOF

:get_last_frame
	rem Get the last frame from given input
	rem 
	rem NOTE: We use temporary files here.
	rem
	rem Args:
	rem * input filename
	rem * input length (for faster last frame capturing)
	rem * output filename
	set input=%~1
	set length=%~2
	set output=%~3

	rem get last second of video
	set /A seek_to=%length%-1
	ffmpeg -ss %seek_to% -i %input% -f image2 -updatefirst 1 %output%
	GOTO:EOF

:image_to_video
	rem Populate video clip from given image
	rem 
	rem Args:
	rem * input filename
	rem * output length
	rem * output filename
	set input=%~1
	set length=%~2
	set output=%~3

	ffmpeg -loop 1 -f image2 -i %input% -c:v libx264 -t %length% %output%
	GOTO:EOF

