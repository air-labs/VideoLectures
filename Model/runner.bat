set logfile=errors.log
set folder=11

call :do_stuff >%logfile% 2>&1
exit /b

:do_stuff

REM cleanup
	cd %folder%
	%windir%\system32\attrib +r desktop.avi
	%windir%\system32\attrib +r face.mp4
	%windir%\system32\attrib +r log.txt
	%windir%\system32\attrib +r montage.editor
	%windir%\system32\attrib +r times.txt
	%windir%\system32\attrib +r titles.txt
	del /s /q *
	%windir%\system32\attrib -r desktop.avi
	%windir%\system32\attrib -r face.mp4
	%windir%\system32\attrib -r log.txt
	%windir%\system32\attrib -r montage.editor
	%windir%\system32\attrib -r times.txt
	%windir%\system32\attrib -r titles.txt
	cd ..
	call pull
	
REM working
	REM make "-converted" sources
	call Recode %folder%
	REM make chunks
	call MakeHigh %folder%

exit /b


