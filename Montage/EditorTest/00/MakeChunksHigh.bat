rmdir /s /q chunks
mkdir chunks
cd chunks
ffmpeg -i ..\face-converted.avi -ss 38.089 -t 9.478 -qscale 0 chunk003.avi
ffmpeg -i ..\face-converted.avi -ss 55.678 -t 43.710 -qscale 0 chunk005.avi
ffmpeg -i ..\face-converted.avi -ss 104.395 -t 16.003 -qscale 0 chunk007.avi
ffmpeg -i ..\face-converted.avi -ss 131.068 -t 6.546 -qscale 0 chunk009.avi
ffmpeg -i ..\face-converted.avi -ss 139.509 -t 3.872 -qscale 0 chunk011.avi
ffmpeg -i ..\face-converted.avi -ss 151.524 -t 13.469 -qscale 0 chunk013.avi
ffmpeg -i ..\face-converted.avi -ss 171.487 -t 3.814 -qscale 0 chunk015.avi
ffmpeg -i ..\face-converted.avi -ss 179.573 -t 10.883 -qscale 0 chunk017.avi
ffmpeg -i ..\face-converted.avi -ss 216.547 -t 10.622 -qscale 0 chunk019.avi
ffmpeg -i ..\face-converted.avi -ss 245.157 -t 38.904 -qscale 0 chunk021.avi
ffmpeg -i ..\face-converted.avi -ss 297.388 -t 6.297 -qscale 0 chunk023.avi
ffmpeg -i ..\face-converted.avi -ss 335.662 -t 14.432 -qscale 0 chunk025.avi
ffmpeg -i ..\face-converted.avi -ss 352.721 -t 19.955 -vn -qscale 0 audio027.avi
ffmpeg -i ..\desktop-converted.avi -ss 338.098 -t 19.955 -qscale 0 video027.avi
ffmpeg -i audio027.avi -i video027.avi -acodec copy -vcodec copy chunk027.avi
ffmpeg -i ..\face-converted.avi -ss 375.257 -t 37.331 -qscale 0 chunk029.avi
ffmpeg -i ..\face-converted.avi -ss 414.573 -t 70.826 -qscale 0 chunk031.avi
ffmpeg -i ..\face-converted.avi -ss 488.186 -t 9.179 -vn -qscale 0 audio033.avi
ffmpeg -i ..\desktop-converted.avi -ss 473.563 -t 9.179 -qscale 0 video033.avi
ffmpeg -i audio033.avi -i video033.avi -acodec copy -vcodec copy chunk033.avi
ffmpeg -i ..\face-converted.avi -ss 500.873 -t 4.489 -vn -qscale 0 audio035.avi
ffmpeg -i ..\desktop-converted.avi -ss 486.250 -t 4.489 -qscale 0 video035.avi
ffmpeg -i audio035.avi -i video035.avi -acodec copy -vcodec copy chunk035.avi
ffmpeg -i ..\face-converted.avi -ss 509.913 -t 8.406 -qscale 0 chunk037.avi
ffmpeg -i ..\face-converted.avi -ss 523.864 -t 10.509 -qscale 0 chunk039.avi
ffmpeg -i ..\face-converted.avi -ss 536.074 -t 8.259 -qscale 0 chunk041.avi
ffmpeg -i ..\face-converted.avi -ss 544.333 -t 3.384 -qscale 0 chunk042.avi
ffmpeg -i ..\face-converted.avi -ss 547.717 -t 11.059 -vn -qscale 0 audio043.avi
ffmpeg -i ..\desktop-converted.avi -ss 533.094 -t 11.059 -qscale 0 video043.avi
ffmpeg -i audio043.avi -i video043.avi -acodec copy -vcodec copy chunk043.avi
ffmpeg -i ..\face-converted.avi -ss 566.033 -t 34.069 -qscale 0 chunk045.avi
ffmpeg -i ..\face-converted.avi -ss 600.102 -t 11.601 -vn -qscale 0 audio046.avi
ffmpeg -i ..\desktop-converted.avi -ss 585.479 -t 11.601 -qscale 0 video046.avi
ffmpeg -i audio046.avi -i video046.avi -acodec copy -vcodec copy chunk046.avi
ffmpeg -i ..\face-converted.avi -ss 611.703 -t 10.624 -qscale 0 chunk047.avi
ffmpeg -i ..\face-converted.avi -ss 632.680 -t 20.205 -vn -qscale 0 audio049.avi
ffmpeg -i ..\desktop-converted.avi -ss 618.057 -t 20.205 -qscale 0 video049.avi
ffmpeg -i audio049.avi -i video049.avi -acodec copy -vcodec copy chunk049.avi
ffmpeg -i ..\face-converted.avi -ss 664.930 -t 15.762 -qscale 0 chunk051.avi
ffmpeg -i ..\face-converted.avi -ss 688.729 -t 10.126 -qscale 0 chunk053.avi
ffmpeg -i ..\face-converted.avi -ss 698.855 -t 13.603 -qscale 0 chunk054.avi
ffmpeg -i ..\face-converted.avi -ss 716.805 -t 4.550 -vn -qscale 0 audio056.avi
ffmpeg -i ..\desktop-converted.avi -ss 702.182 -t 4.550 -qscale 0 video056.avi
ffmpeg -i audio056.avi -i video056.avi -acodec copy -vcodec copy chunk056.avi
ffmpeg -i ..\face-converted.avi -ss 733.437 -t 13.320 -vn -qscale 0 audio058.avi
ffmpeg -i ..\desktop-converted.avi -ss 718.814 -t 13.320 -qscale 0 video058.avi
ffmpeg -i audio058.avi -i video058.avi -acodec copy -vcodec copy chunk058.avi
ffmpeg -i ..\face-converted.avi -ss 752.271 -t 8.724 -vn -qscale 0 audio060.avi
ffmpeg -i ..\desktop-converted.avi -ss 737.648 -t 8.724 -qscale 0 video060.avi
ffmpeg -i audio060.avi -i video060.avi -acodec copy -vcodec copy chunk060.avi
ffmpeg -i ..\face-converted.avi -ss 782.951 -t 7.736 -vn -qscale 0 audio062.avi
ffmpeg -i ..\desktop-converted.avi -ss 768.328 -t 7.736 -qscale 0 video062.avi
ffmpeg -i audio062.avi -i video062.avi -acodec copy -vcodec copy chunk062.avi
ffmpeg -i ..\face-converted.avi -ss 796.061 -t 60.521 -vn -qscale 0 audio064.avi
ffmpeg -i ..\desktop-converted.avi -ss 781.438 -t 60.521 -qscale 0 video064.avi
ffmpeg -i audio064.avi -i video064.avi -acodec copy -vcodec copy chunk064.avi
ffmpeg -i ..\face-converted.avi -ss 866.954 -t 29.380 -vn -qscale 0 audio066.avi
ffmpeg -i ..\desktop-converted.avi -ss 852.331 -t 29.380 -qscale 0 video066.avi
ffmpeg -i audio066.avi -i video066.avi -acodec copy -vcodec copy chunk066.avi
ffmpeg -i ..\face-converted.avi -ss 903.519 -t 5.281 -vn -qscale 0 audio068.avi
ffmpeg -i ..\desktop-converted.avi -ss 888.896 -t 5.281 -qscale 0 video068.avi
ffmpeg -i audio068.avi -i video068.avi -acodec copy -vcodec copy chunk068.avi
ffmpeg -i ..\face-converted.avi -ss 913.904 -t 52.597 -vn -qscale 0 audio070.avi
ffmpeg -i ..\desktop-converted.avi -ss 899.281 -t 52.597 -qscale 0 video070.avi
ffmpeg -i audio070.avi -i video070.avi -acodec copy -vcodec copy chunk070.avi
ffmpeg -i ..\face-converted.avi -ss 981.164 -t 6.478 -qscale 0 chunk072.avi
ffmpeg -i ..\face-converted.avi -ss 991.731 -t 6.148 -qscale 0 chunk074.avi
ffmpeg -i ..\face-converted.avi -ss 1037.885 -t 0.000 -qscale 0 chunk076.avi
ffmpeg -i ..\face-converted.avi -ss 1037.885 -t 16.673 -qscale 0 chunk077.avi
ffmpeg -i ..\face-converted.avi -ss 1076.742 -t 19.390 -qscale 0 chunk079.avi
ffmpeg -i ..\face-converted.avi -ss 1113.627 -t 18.958 -qscale 0 chunk081.avi
ffmpeg -i ..\face-converted.avi -ss 1132.585 -t 32.576 -qscale 0 chunk082.avi
ffmpeg -i ..\face-converted.avi -ss 1165.161 -t 7.980 -vn -qscale 0 audio083.avi
ffmpeg -i ..\desktop-converted.avi -ss 1150.538 -t 7.980 -qscale 0 video083.avi
ffmpeg -i audio083.avi -i video083.avi -acodec copy -vcodec copy chunk083.avi
ffmpeg -i ..\face-converted.avi -ss 1173.141 -t 7.717 -qscale 0 chunk084.avi
ffmpeg -i ..\face-converted.avi -ss 1196.725 -t 7.783 -vn -qscale 0 audio086.avi
ffmpeg -i ..\desktop-converted.avi -ss 1182.102 -t 7.783 -qscale 0 video086.avi
ffmpeg -i audio086.avi -i video086.avi -acodec copy -vcodec copy chunk086.avi
ffmpeg -i ..\face-converted.avi -ss 1215.560 -t 6.671 -qscale 0 chunk088.avi
ffmpeg -i ..\face-converted.avi -ss 1225.802 -t 37.810 -qscale 0 chunk090.avi
ffmpeg -i ..\face-converted.avi -ss 1278.718 -t 19.022 -qscale 0 chunk092.avi
ffmpeg -i ..\face-converted.avi -ss 1301.188 -t 56.200 -qscale 0 chunk094.avi
ffmpeg -i ..\face-converted.avi -ss 1405.515 -t 8.032 -qscale 0 chunk096.avi
ffmpeg -i ..\face-converted.avi -ss 1413.547 -t 28.445 -vn -qscale 0 audio097.avi
ffmpeg -i ..\desktop-converted.avi -ss 1398.924 -t 28.445 -qscale 0 video097.avi
ffmpeg -i audio097.avi -i video097.avi -acodec copy -vcodec copy chunk097.avi
ffmpeg -i ..\face-converted.avi -ss 1444.525 -t 5.094 -qscale 0 chunk099.avi
ffmpeg -i ..\face-converted.avi -ss 1465.662 -t 17.751 -qscale 0 chunk101.avi
ffmpeg -i ..\face-converted.avi -ss 1501.589 -t 16.484 -vn -qscale 0 audio103.avi
ffmpeg -i ..\desktop-converted.avi -ss 1486.966 -t 16.484 -qscale 0 video103.avi
ffmpeg -i audio103.avi -i video103.avi -acodec copy -vcodec copy chunk103.avi
ffmpeg -i ..\face-converted.avi -ss 1521.866 -t 48.824 -vn -qscale 0 audio105.avi
ffmpeg -i ..\desktop-converted.avi -ss 1507.243 -t 48.824 -qscale 0 video105.avi
ffmpeg -i audio105.avi -i video105.avi -acodec copy -vcodec copy chunk105.avi
ffmpeg -i ..\face-converted.avi -ss 1585.900 -t 71.115 -vn -qscale 0 audio107.avi
ffmpeg -i ..\desktop-converted.avi -ss 1571.277 -t 71.115 -qscale 0 video107.avi
ffmpeg -i audio107.avi -i video107.avi -acodec copy -vcodec copy chunk107.avi
ffmpeg -i ..\face-converted.avi -ss 1657.015 -t 4.628 -qscale 0 chunk108.avi
ffmpeg -i ..\face-converted.avi -ss 1684.087 -t 49.524 -vn -qscale 0 audio110.avi
ffmpeg -i ..\desktop-converted.avi -ss 1669.464 -t 49.524 -qscale 0 video110.avi
ffmpeg -i audio110.avi -i video110.avi -acodec copy -vcodec copy chunk110.avi
ffmpeg -i ..\face-converted.avi -ss 1736.588 -t 58.625 -vn -qscale 0 audio112.avi
ffmpeg -i ..\desktop-converted.avi -ss 1721.965 -t 58.625 -qscale 0 video112.avi
ffmpeg -i audio112.avi -i video112.avi -acodec copy -vcodec copy chunk112.avi
ffmpeg -i ..\face-converted.avi -ss 1797.934 -t 12.183 -vn -qscale 0 audio114.avi
ffmpeg -i ..\desktop-converted.avi -ss 1783.311 -t 12.183 -qscale 0 video114.avi
ffmpeg -i audio114.avi -i video114.avi -acodec copy -vcodec copy chunk114.avi
ffmpeg -i ..\face-converted.avi -ss 1811.445 -t 13.315 -vn -qscale 0 audio116.avi
ffmpeg -i ..\desktop-converted.avi -ss 1796.822 -t 13.315 -qscale 0 video116.avi
ffmpeg -i audio116.avi -i video116.avi -acodec copy -vcodec copy chunk116.avi
ffmpeg -i ..\face-converted.avi -ss 1833.832 -t 19.623 -vn -qscale 0 audio118.avi
ffmpeg -i ..\desktop-converted.avi -ss 1819.209 -t 19.623 -qscale 0 video118.avi
ffmpeg -i audio118.avi -i video118.avi -acodec copy -vcodec copy chunk118.avi
ffmpeg -i ..\face-converted.avi -ss 1861.922 -t 7.291 -vn -qscale 0 audio120.avi
ffmpeg -i ..\desktop-converted.avi -ss 1847.299 -t 7.291 -qscale 0 video120.avi
ffmpeg -i audio120.avi -i video120.avi -acodec copy -vcodec copy chunk120.avi
ffmpeg -i ..\face-converted.avi -ss 1872.524 -t 22.248 -vn -qscale 0 audio122.avi
ffmpeg -i ..\desktop-converted.avi -ss 1857.901 -t 22.248 -qscale 0 video122.avi
ffmpeg -i audio122.avi -i video122.avi -acodec copy -vcodec copy chunk122.avi
ffmpeg -i ..\face-converted.avi -ss 1898.442 -t 59.077 -vn -qscale 0 audio124.avi
ffmpeg -i ..\desktop-converted.avi -ss 1883.819 -t 59.077 -qscale 0 video124.avi
ffmpeg -i audio124.avi -i video124.avi -acodec copy -vcodec copy chunk124.avi
ffmpeg -i ..\face-converted.avi -ss 1991.922 -t 27.212 -qscale 0 chunk126.avi
ffmpeg -i ..\face-converted.avi -ss 2073.362 -t 0.000 -qscale 0 chunk128.avi
ffmpeg -i ..\face-converted.avi -ss 2073.362 -t 14.262 -qscale 0 chunk129.avi
ffmpeg -i ..\face-converted.avi -ss 2135.705 -t 73.039 -qscale 0 chunk131.avi
ffmpeg -i ..\face-converted.avi -ss 2221.923 -t 50.898 -qscale 0 chunk133.avi
ffmpeg -i ..\face-converted.avi -ss 2272.821 -t 2.696 -qscale 0 chunk134.avi
ffmpeg -i ..\face-converted.avi -ss 2283.173 -t 81.925 -vn -qscale 0 audio136.avi
ffmpeg -i ..\desktop-converted.avi -ss 2268.550 -t 81.925 -qscale 0 video136.avi
ffmpeg -i audio136.avi -i video136.avi -acodec copy -vcodec copy chunk136.avi
ffmpeg -i ..\face-converted.avi -ss 2365.098 -t 11.652 -vn -qscale 0 audio137.avi
ffmpeg -i ..\desktop-converted.avi -ss 2350.475 -t 11.652 -qscale 0 video137.avi
ffmpeg -i audio137.avi -i video137.avi -acodec copy -vcodec copy chunk137.avi
ffmpeg -i ..\face-converted.avi -ss 2376.750 -t 63.546 -qscale 0 chunk138.avi
ffmpeg -i ..\face-converted.avi -ss 2463.093 -t 9.926 -vn -qscale 0 audio140.avi
ffmpeg -i ..\desktop-converted.avi -ss 2448.470 -t 9.926 -qscale 0 video140.avi
ffmpeg -i audio140.avi -i video140.avi -acodec copy -vcodec copy chunk140.avi
ffmpeg -i ..\face-converted.avi -ss 2487.952 -t 19.614 -vn -qscale 0 audio142.avi
ffmpeg -i ..\desktop-converted.avi -ss 2473.329 -t 19.614 -qscale 0 video142.avi
ffmpeg -i audio142.avi -i video142.avi -acodec copy -vcodec copy chunk142.avi
ffmpeg -i ..\face-converted.avi -ss 2507.566 -t 16.950 -vn -qscale 0 audio143.avi
ffmpeg -i ..\desktop-converted.avi -ss 2492.943 -t 16.950 -qscale 0 video143.avi
ffmpeg -i audio143.avi -i video143.avi -acodec copy -vcodec copy chunk143.avi
ffmpeg -i ..\face-converted.avi -ss 2524.516 -t 10.516 -qscale 0 chunk144.avi
ffmpeg -i ..\face-converted.avi -ss 2558.219 -t 23.175 -vn -qscale 0 audio146.avi
ffmpeg -i ..\desktop-converted.avi -ss 2543.596 -t 23.175 -qscale 0 video146.avi
ffmpeg -i audio146.avi -i video146.avi -acodec copy -vcodec copy chunk146.avi
ffmpeg -i ..\face-converted.avi -ss 2583.450 -t 23.030 -vn -qscale 0 audio148.avi
ffmpeg -i ..\desktop-converted.avi -ss 2568.827 -t 23.030 -qscale 0 video148.avi
ffmpeg -i audio148.avi -i video148.avi -acodec copy -vcodec copy chunk148.avi
ffmpeg -i ..\face-converted.avi -ss 2610.219 -t 3.083 -vn -qscale 0 audio150.avi
ffmpeg -i ..\desktop-converted.avi -ss 2595.596 -t 3.083 -qscale 0 video150.avi
ffmpeg -i audio150.avi -i video150.avi -acodec copy -vcodec copy chunk150.avi
ffmpeg -i ..\face-converted.avi -ss 2619.004 -t 17.015 -qscale 0 chunk152.avi
ffmpeg -i ..\face-converted.avi -ss 2649.418 -t 20.013 -qscale 0 chunk154.avi
cd ..
