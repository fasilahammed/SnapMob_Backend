@echo off
cd /d "C:\Users\FASIL\source\repos\SnapMob_Backend\SnapMob_Backend\daily task"
echo Auto commit at %date% %time% >> logs.txt
git add .
git commit -m "Log Done on %date% %time%"
git push origin main
exit
