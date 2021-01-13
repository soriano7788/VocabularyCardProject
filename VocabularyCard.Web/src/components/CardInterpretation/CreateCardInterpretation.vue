<template>
  <div class="bg-light pt-3 mt-3">
    <div class="row mb-3">
      <label for="part-of-speech" class="col-form-label col-md-4">詞性</label>
      <div class="col-md-8">
        <select
          name=""
          id="part-of-speech"
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
      <label for="phonetic-symbol" class="col-form-label col-md-4">音標</label>
      <div class="col-md-8">
        <input
          type="text"
          class="form-control"
          id="phonetic-symbol"
          v-model="interpret.PhoneticSymbol"
        />
      </div>
    </div>

    <div class="row mb-3">
      <label for="interpret" class="col-form-label col-md-4">解釋</label>
      <div class="col-md-8">
        <input
          type="text"
          class="form-control"
          id="interpret"
          v-model="interpret.Interpretation"
        />
      </div>
    </div>

    <div class="row mb-3">
      <label for="example-sentence" class="col-form-label col-md-4">例句</label>
      <div class="col-md-8">
        <input
          type="text"
          class="form-control"
          id="example-sentence"
          v-model="interpret.ExampleSentence"
        />
      </div>
    </div>

    <div class="row mb-3">
      <label for="example-sentence-explanation" class="col-form-label col-md-4"
        >例句說明</label
      >
      <div class="col-md-8">
        <input
          type="text"
          class="form-control"
          id="example-sentence-explanation"
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
  computed: {
    partOfSpeechList: function() {
      return PartOfSpeech;
    },
  },
};
</script>
