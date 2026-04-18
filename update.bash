cwd=$(pwd)
GAMEPATH=Kingdoms\ and\ Castles/
STEAMPATH=/c/Games/Steam/steamapps/common/
MODPATH="/c/Games/Steam/steamapps/common/Kingdoms and Castles/KingdomsAndCastles_Data/mods"


cd $STEAMPATH || exit
cd "$GAMEPATH" || exit
cd "KingdomsAndCastles_Data/mods" || exit

cp $cwd/BigPeopleMod.cs .
start .