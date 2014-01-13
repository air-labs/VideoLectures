rmdir /s /q chunks
mkdir chunks
cd chunks
ffmpeg -i ..\face-converted.avi -ss 38.089 -t 9.478 -acodec copy -vcodec copy chunk003.avi
ffmpeg -i ..\face-converted.avi -ss 55.678 -t 43.710 -acodec copy -vcodec copy chunk005.avi
ffmpeg -i ..\face-converted.avi -ss 104.395 -t 16.003 -acodec copy -vcodec copy chunk007.avi
ffmpeg -i ..\face-converted.avi -ss 131.068 -t 6.546 -acodec copy -vcodec copy chunk009.avi
ffmpeg -i ..\face-converted.avi -ss 139.509 -t 3.872 -acodec copy -vcodec copy chunk011.avi
ffmpeg -i ..\face-converted.avi -ss 151.524 -t 13.469 -acodec copy -vcodec copy chunk013.avi
ffmpeg -i ..\face-converted.avi -ss 171.487 -t 3.814 -acodec copy -vcodec copy chunk015.avi
ffmpeg -i ..\face-converted.avi -ss 179.573 -t 10.883 -acodec copy -vcodec copy chunk017.avi
ffmpeg -i ..\face-converted.avi -ss 216.547 -t 10.622 -acodec copy -vcodec copy chunk019.avi
ffmpeg -i ..\face-converted.avi -ss 245.157 -t 38.904 -acodec copy -vcodec copy chunk021.avi
ffmpeg -i ..\face-converted.avi -ss 297.388 -t 6.297 -acodec copy -vcodec copy chunk023.avi
ffmpeg -i ..\face-converted.avi -ss 335.662 -t 14.432 -acodec copy -vcodec copy chunk025.avi
ffmpeg -i ..\face-converted.avi -ss 352.721 -t 19.955 -acodec copy -vn audio027.avi
ffmpeg -i ..\desktop-converted.avi -ss 338.098 -t 19.955 -acodec copy -vcodec copy video027.avi
ffmpeg -i audio027.avi -i video027.avi -acodec copy -vcodec copy chunk027.avi
ffmpeg -i ..\face-converted.avi -ss 375.257 -t 37.331 -acodec copy -vcodec copy chunk029.avi
ffmpeg -i ..\face-converted.avi -ss 414.573 -t 70.826 -acodec copy -vcodec copy chunk031.avi
ffmpeg -i ..\face-converted.avi -ss 488.186 -t 9.179 -acodec copy -vn audio033.avi
ffmpeg -i ..\desktop-converted.avi -ss 473.563 -t 9.179 -acodec copy -vcodec copy video033.avi
ffmpeg -i audio033.avi -i video033.avi -acodec copy -vcodec copy chunk033.avi
ffmpeg -i ..\face-converted.avi -ss 500.873 -t 4.489 -acodec copy -vn audio035.avi
ffmpeg -i ..\desktop-converted.avi -ss 486.250 -t 4.489 -acodec copy -vcodec copy video035.avi
ffmpeg -i audio035.avi -i video035.avi -acodec copy -vcodec copy chunk035.avi
ffmpeg -i ..\face-converted.avi -ss 509.913 -t 8.406 -acodec copy -vcodec copy chunk037.avi
ffmpeg -i ..\face-converted.avi -ss 523.864 -t 10.509 -acodec copy -vcodec copy chunk039.avi
ffmpeg -i ..\face-converted.avi -ss 536.074 -t 8.259 -acodec copy -vcodec copy chunk041.avi
ffmpeg -i ..\face-converted.avi -ss 544.333 -t 3.384 -acodec copy -vcodec copy chunk042.avi
ffmpeg -i ..\face-converted.avi -ss 547.717 -t 11.059 -acodec copy -vn audio043.avi
ffmpeg -i ..\desktop-converted.avi -ss 533.094 -t 11.059 -acodec copy -vcodec copy video043.avi
ffmpeg -i audio043.avi -i video043.avi -acodec copy -vcodec copy chunk043.avi
ffmpeg -i ..\face-converted.avi -ss 566.033 -t 34.069 -acodec copy -vcodec copy chunk045.avi
ffmpeg -i ..\face-converted.avi -ss 600.102 -t 11.601 -acodec copy -vn audio046.avi
ffmpeg -i ..\desktop-converted.avi -ss 585.479 -t 11.601 -acodec copy -vcodec copy video046.avi
ffmpeg -i audio046.avi -i video046.avi -acodec copy -vcodec copy chunk046.avi
ffmpeg -i ..\face-converted.avi -ss 611.703 -t 10.624 -acodec copy -vcodec copy chunk047.avi
ffmpeg -i ..\face-converted.avi -ss 632.680 -t 20.205 -acodec copy -vn audio049.avi
ffmpeg -i ..\desktop-converted.avi -ss 618.057 -t 20.205 -acodec copy -vcodec copy video049.avi
ffmpeg -i audio049.avi -i video049.avi -acodec copy -vcodec copy chunk049.avi
ffmpeg -i ..\face-converted.avi -ss 664.930 -t 15.762 -acodec copy -vcodec copy chunk051.avi
ffmpeg -i ..\face-converted.avi -ss 688.729 -t 10.126 -acodec copy -vcodec copy chunk053.avi
ffmpeg -i ..\face-converted.avi -ss 698.855 -t 13.603 -acodec copy -vcodec copy chunk054.avi
ffmpeg -i ..\face-converted.avi -ss 716.805 -t 4.550 -acodec copy -vn audio056.avi
ffmpeg -i ..\desktop-converted.avi -ss 702.182 -t 4.550 -acodec copy -vcodec copy video056.avi
ffmpeg -i audio056.avi -i video056.avi -acodec copy -vcodec copy chunk056.avi
ffmpeg -i ..\face-converted.avi -ss 733.437 -t 13.320 -acodec copy -vn audio058.avi
ffmpeg -i ..\desktop-converted.avi -ss 718.814 -t 13.320 -acodec copy -vcodec copy video058.avi
ffmpeg -i audio058.avi -i video058.avi -acodec copy -vcodec copy chunk058.avi
ffmpeg -i ..\face-converted.avi -ss 752.271 -t 8.724 -acodec copy -vn audio060.avi
ffmpeg -i ..\desktop-converted.avi -ss 737.648 -t 8.724 -acodec copy -vcodec copy video060.avi
ffmpeg -i audio060.avi -i video060.avi -acodec copy -vcodec copy chunk060.avi
ffmpeg -i ..\face-converted.avi -ss 782.951 -t 7.736 -acodec copy -vn audio062.avi
ffmpeg -i ..\desktop-converted.avi -ss 768.328 -t 7.736 -acodec copy -vcodec copy video062.avi
ffmpeg -i audio062.avi -i video062.avi -acodec copy -vcodec copy chunk062.avi
ffmpeg -i ..\face-converted.avi -ss 796.061 -t 60.521 -acodec copy -vn audio064.avi
ffmpeg -i ..\desktop-converted.avi -ss 781.438 -t 60.521 -acodec copy -vcodec copy video064.avi
ffmpeg -i audio064.avi -i video064.avi -acodec copy -vcodec copy chunk064.avi
ffmpeg -i ..\face-converted.avi -ss 866.954 -t 29.380 -acodec copy -vn audio066.avi
ffmpeg -i ..\desktop-converted.avi -ss 852.331 -t 29.380 -acodec copy -vcodec copy video066.avi
ffmpeg -i audio066.avi -i video066.avi -acodec copy -vcodec copy chunk066.avi
ffmpeg -i ..\face-converted.avi -ss 903.519 -t 5.281 -acodec copy -vn audio068.avi
ffmpeg -i ..\desktop-converted.avi -ss 888.896 -t 5.281 -acodec copy -vcodec copy video068.avi
ffmpeg -i audio068.avi -i video068.avi -acodec copy -vcodec copy chunk068.avi
ffmpeg -i ..\face-converted.avi -ss 913.904 -t 52.597 -acodec copy -vn audio070.avi
ffmpeg -i ..\desktop-converted.avi -ss 899.281 -t 52.597 -acodec copy -vcodec copy video070.avi
ffmpeg -i audio070.avi -i video070.avi -acodec copy -vcodec copy chunk070.avi
ffmpeg -i ..\face-converted.avi -ss 981.164 -t 6.478 -acodec copy -vcodec copy chunk072.avi
ffmpeg -i ..\face-converted.avi -ss 991.731 -t 6.148 -acodec copy -vcodec copy chunk074.avi
ffmpeg -i ..\face-converted.avi -ss 1037.885 -t 0.000 -acodec copy -vcodec copy chunk076.avi
ffmpeg -i ..\face-converted.avi -ss 1037.885 -t 16.673 -acodec copy -vcodec copy chunk077.avi
ffmpeg -i ..\face-converted.avi -ss 1076.742 -t 19.390 -acodec copy -vcodec copy chunk079.avi
ffmpeg -i ..\face-converted.avi -ss 1113.627 -t 18.958 -acodec copy -vcodec copy chunk081.avi
ffmpeg -i ..\face-converted.avi -ss 1132.585 -t 32.576 -acodec copy -vcodec copy chunk082.avi
ffmpeg -i ..\face-converted.avi -ss 1165.161 -t 7.980 -acodec copy -vn audio083.avi
ffmpeg -i ..\desktop-converted.avi -ss 1150.538 -t 7.980 -acodec copy -vcodec copy video083.avi
ffmpeg -i audio083.avi -i video083.avi -acodec copy -vcodec copy chunk083.avi
ffmpeg -i ..\face-converted.avi -ss 1173.141 -t 7.717 -acodec copy -vcodec copy chunk084.avi
ffmpeg -i ..\face-converted.avi -ss 1196.725 -t 7.783 -acodec copy -vn audio086.avi
ffmpeg -i ..\desktop-converted.avi -ss 1182.102 -t 7.783 -acodec copy -vcodec copy video086.avi
ffmpeg -i audio086.avi -i video086.avi -acodec copy -vcodec copy chunk086.avi
ffmpeg -i ..\face-converted.avi -ss 1215.560 -t 6.671 -acodec copy -vcodec copy chunk088.avi
ffmpeg -i ..\face-converted.avi -ss 1225.802 -t 37.810 -acodec copy -vcodec copy chunk090.avi
ffmpeg -i ..\face-converted.avi -ss 1278.718 -t 19.022 -acodec copy -vcodec copy chunk092.avi
ffmpeg -i ..\face-converted.avi -ss 1301.188 -t 56.200 -acodec copy -vcodec copy chunk094.avi
ffmpeg -i ..\face-converted.avi -ss 1405.515 -t 8.032 -acodec copy -vcodec copy chunk096.avi
ffmpeg -i ..\face-converted.avi -ss 1413.547 -t 28.445 -acodec copy -vn audio097.avi
ffmpeg -i ..\desktop-converted.avi -ss 1398.924 -t 28.445 -acodec copy -vcodec copy video097.avi
ffmpeg -i audio097.avi -i video097.avi -acodec copy -vcodec copy chunk097.avi
ffmpeg -i ..\face-converted.avi -ss 1444.525 -t 5.094 -acodec copy -vcodec copy chunk099.avi
ffmpeg -i ..\face-converted.avi -ss 1465.662 -t 17.751 -acodec copy -vcodec copy chunk101.avi
ffmpeg -i ..\face-converted.avi -ss 1501.589 -t 16.484 -acodec copy -vn audio103.avi
ffmpeg -i ..\desktop-converted.avi -ss 1486.966 -t 16.484 -acodec copy -vcodec copy video103.avi
ffmpeg -i audio103.avi -i video103.avi -acodec copy -vcodec copy chunk103.avi
ffmpeg -i ..\face-converted.avi -ss 1521.866 -t 48.824 -acodec copy -vn audio105.avi
ffmpeg -i ..\desktop-converted.avi -ss 1507.243 -t 48.824 -acodec copy -vcodec copy video105.avi
ffmpeg -i audio105.avi -i video105.avi -acodec copy -vcodec copy chunk105.avi
ffmpeg -i ..\face-converted.avi -ss 1585.900 -t 71.115 -acodec copy -vn audio107.avi
ffmpeg -i ..\desktop-converted.avi -ss 1571.277 -t 71.115 -acodec copy -vcodec copy video107.avi
ffmpeg -i audio107.avi -i video107.avi -acodec copy -vcodec copy chunk107.avi
ffmpeg -i ..\face-converted.avi -ss 1657.015 -t 4.628 -acodec copy -vcodec copy chunk108.avi
ffmpeg -i ..\face-converted.avi -ss 1684.087 -t 49.524 -acodec copy -vn audio110.avi
ffmpeg -i ..\desktop-converted.avi -ss 1669.464 -t 49.524 -acodec copy -vcodec copy video110.avi
ffmpeg -i audio110.avi -i video110.avi -acodec copy -vcodec copy chunk110.avi
ffmpeg -i ..\face-converted.avi -ss 1736.588 -t 58.625 -acodec copy -vn audio112.avi
ffmpeg -i ..\desktop-converted.avi -ss 1721.965 -t 58.625 -acodec copy -vcodec copy video112.avi
ffmpeg -i audio112.avi -i video112.avi -acodec copy -vcodec copy chunk112.avi
ffmpeg -i ..\face-converted.avi -ss 1797.934 -t 12.183 -acodec copy -vn audio114.avi
ffmpeg -i ..\desktop-converted.avi -ss 1783.311 -t 12.183 -acodec copy -vcodec copy video114.avi
ffmpeg -i audio114.avi -i video114.avi -acodec copy -vcodec copy chunk114.avi
ffmpeg -i ..\face-converted.avi -ss 1811.445 -t 13.315 -acodec copy -vn audio116.avi
ffmpeg -i ..\desktop-converted.avi -ss 1796.822 -t 13.315 -acodec copy -vcodec copy video116.avi
ffmpeg -i audio116.avi -i video116.avi -acodec copy -vcodec copy chunk116.avi
ffmpeg -i ..\face-converted.avi -ss 1833.832 -t 19.623 -acodec copy -vn audio118.avi
ffmpeg -i ..\desktop-converted.avi -ss 1819.209 -t 19.623 -acodec copy -vcodec copy video118.avi
ffmpeg -i audio118.avi -i video118.avi -acodec copy -vcodec copy chunk118.avi
ffmpeg -i ..\face-converted.avi -ss 1861.922 -t 7.291 -acodec copy -vn audio120.avi
ffmpeg -i ..\desktop-converted.avi -ss 1847.299 -t 7.291 -acodec copy -vcodec copy video120.avi
ffmpeg -i audio120.avi -i video120.avi -acodec copy -vcodec copy chunk120.avi
ffmpeg -i ..\face-converted.avi -ss 1872.524 -t 22.248 -acodec copy -vn audio122.avi
ffmpeg -i ..\desktop-converted.avi -ss 1857.901 -t 22.248 -acodec copy -vcodec copy video122.avi
ffmpeg -i audio122.avi -i video122.avi -acodec copy -vcodec copy chunk122.avi
ffmpeg -i ..\face-converted.avi -ss 1898.442 -t 59.077 -acodec copy -vn audio124.avi
ffmpeg -i ..\desktop-converted.avi -ss 1883.819 -t 59.077 -acodec copy -vcodec copy video124.avi
ffmpeg -i audio124.avi -i video124.avi -acodec copy -vcodec copy chunk124.avi
ffmpeg -i ..\face-converted.avi -ss 1991.922 -t 27.212 -acodec copy -vcodec copy chunk126.avi
ffmpeg -i ..\face-converted.avi -ss 2073.362 -t 0.000 -acodec copy -vcodec copy chunk128.avi
ffmpeg -i ..\face-converted.avi -ss 2073.362 -t 14.262 -acodec copy -vcodec copy chunk129.avi
ffmpeg -i ..\face-converted.avi -ss 2135.705 -t 73.039 -acodec copy -vcodec copy chunk131.avi
ffmpeg -i ..\face-converted.avi -ss 2221.923 -t 50.898 -acodec copy -vcodec copy chunk133.avi
ffmpeg -i ..\face-converted.avi -ss 2272.821 -t 2.696 -acodec copy -vcodec copy chunk134.avi
ffmpeg -i ..\face-converted.avi -ss 2283.173 -t 81.925 -acodec copy -vn audio136.avi
ffmpeg -i ..\desktop-converted.avi -ss 2268.550 -t 81.925 -acodec copy -vcodec copy video136.avi
ffmpeg -i audio136.avi -i video136.avi -acodec copy -vcodec copy chunk136.avi
ffmpeg -i ..\face-converted.avi -ss 2365.098 -t 11.652 -acodec copy -vn audio137.avi
ffmpeg -i ..\desktop-converted.avi -ss 2350.475 -t 11.652 -acodec copy -vcodec copy video137.avi
ffmpeg -i audio137.avi -i video137.avi -acodec copy -vcodec copy chunk137.avi
ffmpeg -i ..\face-converted.avi -ss 2376.750 -t 63.546 -acodec copy -vcodec copy chunk138.avi
ffmpeg -i ..\face-converted.avi -ss 2463.093 -t 9.926 -acodec copy -vn audio140.avi
ffmpeg -i ..\desktop-converted.avi -ss 2448.470 -t 9.926 -acodec copy -vcodec copy video140.avi
ffmpeg -i audio140.avi -i video140.avi -acodec copy -vcodec copy chunk140.avi
ffmpeg -i ..\face-converted.avi -ss 2487.952 -t 19.614 -acodec copy -vn audio142.avi
ffmpeg -i ..\desktop-converted.avi -ss 2473.329 -t 19.614 -acodec copy -vcodec copy video142.avi
ffmpeg -i audio142.avi -i video142.avi -acodec copy -vcodec copy chunk142.avi
ffmpeg -i ..\face-converted.avi -ss 2507.566 -t 16.950 -acodec copy -vn audio143.avi
ffmpeg -i ..\desktop-converted.avi -ss 2492.943 -t 16.950 -acodec copy -vcodec copy video143.avi
ffmpeg -i audio143.avi -i video143.avi -acodec copy -vcodec copy chunk143.avi
ffmpeg -i ..\face-converted.avi -ss 2524.516 -t 10.516 -acodec copy -vcodec copy chunk144.avi
ffmpeg -i ..\face-converted.avi -ss 2558.219 -t 23.175 -acodec copy -vn audio146.avi
ffmpeg -i ..\desktop-converted.avi -ss 2543.596 -t 23.175 -acodec copy -vcodec copy video146.avi
ffmpeg -i audio146.avi -i video146.avi -acodec copy -vcodec copy chunk146.avi
ffmpeg -i ..\face-converted.avi -ss 2583.450 -t 23.030 -acodec copy -vn audio148.avi
ffmpeg -i ..\desktop-converted.avi -ss 2568.827 -t 23.030 -acodec copy -vcodec copy video148.avi
ffmpeg -i audio148.avi -i video148.avi -acodec copy -vcodec copy chunk148.avi
ffmpeg -i ..\face-converted.avi -ss 2610.219 -t 3.083 -acodec copy -vn audio150.avi
ffmpeg -i ..\desktop-converted.avi -ss 2595.596 -t 3.083 -acodec copy -vcodec copy video150.avi
ffmpeg -i audio150.avi -i video150.avi -acodec copy -vcodec copy chunk150.avi
ffmpeg -i ..\face-converted.avi -ss 2619.004 -t 17.015 -acodec copy -vcodec copy chunk152.avi
ffmpeg -i ..\face-converted.avi -ss 2649.418 -t 20.013 -acodec copy -vcodec copy chunk154.avi
cd ..
