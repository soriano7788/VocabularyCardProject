<template>
  <div class="row bg-create-panel pt-3 px-0 mt-3 mb-3 mx-0">
    <div class="row justify-content-end pb-3">
      <button
        type="button"
        class="btn-close btn-close-white"
        @click="removeInterpret(interpret.Id)"
      ></button>
    </div>
    <div class="row mb-3">
      <label
        :for="'part-of-speech-' + interpret.Id"
        class="form-label col-sm-2"
        >{{ $t("INTERPRET.PART_OF_SPEECH") }}</label
      >
      <div :id="'part-of-speech-' + interpret.Id" class="col-sm-10">
        <select
          name=""
          :id="'part-of-speech-' + interpret.Id"
          class="form-select"
          v-model="interpret.PartOfSpeech"
        >
          <option
            v-for="(value, name) in partOfSpeechList"
            :key="name"
            :selected="name == 0"
            :value="name"
          >
            {{ value }}</option
          >
        </select>
      </div>
    </div>
    <div class="row mb-3">
      <label
        :for="'phonetic-symbol-' + interpret.Id"
        class="form-label col-sm-2"
        >{{ $t("INTERPRET.PHONETIC_SYMBOL") }}</label
      >
      <div class="col-sm-10">
        <input
          type="text"
          class="form-control"
          :id="'phonetic-symbol-' + interpret.Id"
          v-model="interpret.PhoneticSymbol"
        />
      </div>
    </div>
    <div class="row mb-3">
      <label :for="'interpret-' + interpret.Id" class="form-label col-sm-2">
        {{ $t("INTERPRET.INTERPRETATION") }}
      </label>
      <div class="col-sm-10">
        <input
          type="text"
          class="form-control"
          :id="'interpret-' + interpret.Id"
          v-model="interpret.Interpretation"
        />
      </div>
    </div>
    <div class="row mb-3">
      <label
        :for="'example-sentence-' + interpret.Id"
        class="form-label col-sm-2"
        >{{ $t("INTERPRET.EXAMPLE_SENTENCE") }}</label
      >
      <div class="col-sm-10">
        <input
          type="text"
          class="form-control"
          :id="'example-sentence-' + interpret.Id"
          v-model="interpret.ExampleSentence"
        />
      </div>
    </div>
    <div class="row mb-3">
      <label
        :for="'example-sentence-explanation-' + interpret.Id"
        class="col-form-label col-sm-2"
        >{{ $t("INTERPRET.EXAMPLE_SENTENCE_EXPLANATION") }}</label
      >
      <div class="col-sm-10">
        <input
          type="text"
          class="form-control"
          :id="'example-sentence-explanation-' + interpret.Id"
          v-model="interpret.ExampleSentenceExplanation"
        />
      </div>
    </div>
  </div>
</template>

<script>
import PartOfSpeech from "../../constants/PartOfSpeech.js";

export default {
  props: ["interpret"],
  data() {
    return {};
  },
  computed: {
    partOfSpeechList: function() {
      return PartOfSpeech;
    },
  },
  methods: {
    removeInterpret(interpretId) {
      this.$emit("removeInterpretById", interpretId);
    },
  },
};
</script>
