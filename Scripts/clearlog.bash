STEAMPATH=/c/Games/Steam/steamapps/common/
GAMEPATH=Kingdoms\ and\ Castles/

#cd $STEAMPATH || exit
#cd "$GAMEPATH" || exit
#cd "KingdomsAndCastles_Data/mods/KiraMod" || exit

FULLPATH="${STEAMPATH}${GAMEPATH}KingdomsAndCastles_Data/mods/KiraMod"
OUTPUT_LOG="${FULLPATH}/output.txt"
LOGPATH="${FULLPATH}/../log.txt"

printf "\n==========================================\n" >> "$OUTPUT_LOG"
printf "\n==========================================\n" >> "$LOGPATH"