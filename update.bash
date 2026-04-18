cwd=$(pwd)
STEAMPATH=/c/Games/Steam/steamapps/common/
GAMEPATH=Kingdoms\ and\ Castles/

cd $STEAMPATH || exit
cd "$GAMEPATH" || exit
cd "KingdomsAndCastles_Data/mods/KiraMod" || exit

# copy/replace code to game folder
cp $cwd/BigPeopleMod.cs .

# copy/replace info.json to game folder
cp $cwd/info.json .

start .