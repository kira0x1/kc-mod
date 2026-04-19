STEAMPATH=/c/Games/Steam/steamapps/common/
GAMEPATH=Kingdoms\ and\ Castles/
FULLPATH="${STEAMPATH}${GAMEPATH}KingdomsAndCastles_Data/mods/KiraMod"

# copy/replace code to game folder
cp ../GlorpMod.cs "${FULLPATH}/GlorpMod.cs"

# copy/replace info.json to game folder
cp ../info.json "${FULLPATH}/info.json"

cd "$FULLPATH" || exit

start .