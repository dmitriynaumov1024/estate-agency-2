<template>
    <Listbox as="div" class="pos-relative">
        <ListboxButton class="button flex-strip flex-i">
            <span>{{locale.nativeName}}</span>
            <ChevronDownIcon class="icon" />
        </ListboxButton>
        <ListboxOptions as="div" class="dropdown pad-8px">
            <ListboxOption as="span" class="dropdown-item"
                v-for="(val, key) in allLocales" 
                :key="key" :value="key"
                @click="$emit('change', key)">
                {{val.nativeName}}
            </ListboxOption>
        </ListboxOptions>
    </Listbox>
</template>

<script>
import { Listbox, ListboxLabel, ListboxButton, 
    ListboxOptions, ListboxOption } from "@headlessui/vue"

import { ChevronDownIcon } from "@heroicons/vue/solid"

export default {
    inject: ["locale", "allLocales"],
    components: {
        Listbox,
        ListboxLabel,
        ListboxButton,
        ListboxOptions,
        ListboxOption,
        ChevronDownIcon 
    }
}
</script>