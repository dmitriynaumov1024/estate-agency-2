import English from "./localization/english.js"
import Ukrainian from "./localization/ukrainian.js"
import Russian from "./localization/russian.js"

const KNOWN_LOCALES = {
    "english": English,
    "ukrainian": Ukrainian,
    "russian": Russian
}

const DEFAULT_LOCALE_KEY = "english"

var onLocaleChangeCallbacks = []

function getLocaleKey () {
    return window.localStorage["localeKey"]
}

function setLocaleKey (name) {
    name = name.toLowerCase()
    var newLocale = KNOWN_LOCALES[name]
    if (newLocale != undefined) {
        window.localStorage["localeKey"] = name
        localeChanged()
    }
}

function getCurrentLocale () {
    return KNOWN_LOCALES[getLocaleKey()]
}

function localeChanged () {
    onLocaleChangeCallbacks.forEach ( callback => {
        if (typeof callback == "function") {
            callback()
        }
    })
}

if (!getLocaleKey()) {
    setLocaleKey(DEFAULT_LOCALE_KEY)
}

export default {
    all() {
        return KNOWN_LOCALES
    },
    key() {
        return getLocaleKey()
    },
    locale() {
        return getCurrentLocale()
    },
    setLocale(name) {
        setLocaleKey(name)
    },
    onChange(func) {
        onLocaleChangeCallbacks.push(func)
    }
}
