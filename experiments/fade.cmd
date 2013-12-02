@setlocal
@echo off
PATH C:\ffmpeg\bin\

del tmp__*
del test*.mp4

rem call :fadein sample.avi 3 fadein.avi
call :crossfade_frame sample.avi sample.avi 5 3 test_cross.mp4

GOTO:EOF
endlocal

:fadein
	rem Apply fade-in effect on given input
	rem 
	rem Args:
	rem * input filename
	rem * duration of an effect
	rem * output filename
	setlocal
	set input=%~1
	set duration=%~2
	set output=%~3
	rem background color
	rem ffmpeg's default is 'black'
	set color=black
	ffmpeg -i %input% -vf fade=in:st=0:d=%duration%:color=%color% %output%
	endlocal
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
	setlocal
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
	endlocal
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
	setlocal
	set input1=%~1
	set input2=%~2
	set input1_len=%~3
	set duration=%~4
	set output=%~5
	rem store last frame of input1 here
	set tmp_frame=tmp__frame.png
	rem this preserves extension to auto-select output format
	set tmp_video=tmp__%output%
	set tmp_video2=tmp__%input2%
	set tmp_video3=tmp___%input2%
	rem actual magic from docs
	rem note the 'ticks' around expression
	set blend_expression='A*(if(gte(T\,%duration%)\,1\,T/%duration%))+B*(1-(if(gte(T\,%duration%)\,1\,T/%duration%)))'
	rem oh, that's TOO SLOW
	rem and alpha+overlay doesn't work
	rem so to speed up we'll blend only part of video
	rem
	rem parts of filtergraph:
	set FADE=blend=all_expr=%blend_expression%
	set OVERLAY=overlay=repeatlast=0
	
	call :get_last_frame %input1% %input1_len% %tmp_frame%
	call :image_to_video %tmp_frame% %duration% %tmp_video%
	ffmpeg -i %input2% -vcodec copy -an -t %duration% %tmp_video2%
	ffmpeg -i %tmp_video% -i %tmp_video2% -filter_complex "[1:v] [0:v] %FADE%" %tmp_video3%
	ffmpeg -i %input2% -i %tmp_video3% -filter_complex "%OVERLAY%" %output%
	rem ffmpeg -i %input2% -i %tmp_video% -filter_complex "[1:v] [start] %FADE% [faded]; [0:v] %CUT% [start]; [faded] [1:v] %OVERLAY%" %output%
	rem all_expr='A*(if(gte(T,10),1,T/10))+B*(1-(if(gte(T,10),1,T/10)))'
	rem FAIL: [1:v] fade=out:alpha=1:st=0:d=%duration%:color=red@1.0 [faded]; [faded] [0:v] overlay
	endlocal
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
	setlocal
	set input=%~1
	set length=%~2
	set output=%~3

	rem get last second of video
	set /A seek_to=%length%-1
	ffmpeg -noaccurate_seek -ss %seek_to% -i %input% -f image2 -updatefirst 1 %output%
	endlocal
	GOTO:EOF

:image_to_video
	rem Populate video clip from given image
	rem 
	rem Args:
	rem * input filename
	rem * output length
	rem * output filename
	setlocal
	set input=%~1
	set length=%~2
	set output=%~3

	ffmpeg -loop 1 -f image2 -i %input% -t %length% %output%
	endlocal
	GOTO:EOF

