import dateFormat from "dateformat"

export default function (d) {
    if (typeof d == "string") d = new Date(d)
    return dateFormat(d, "yyyy-mm-dd ~ HH:MM")
}