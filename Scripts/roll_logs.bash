STEAMPATH=/c/Games/Steam/steamapps/common/
GAMEPATH=Kingdoms\ and\ Castles/
FULLPATH="${STEAMPATH}${GAMEPATH}KingdomsAndCastles_Data/mods/KiraMod"

OUTPUT_LOG="${FULLPATH}/output.txt"
LOGPATH="${FULLPATH}/../log.txt"

mv "$OUTPUT_LOG" "${FULLPATH}/output_1.txt"
mv "$LOGPATH" "${FULLPATH}/../log_1.txt"