<template>
  <li class="list-group-item">
    <span class="badge bg-success">
      {{ interpretation.PartOfSpeech | partOfSpeechToString }}
    </span>
    <span class="phontic-synbol">
      {{ interpretation.PhoneticSymbol }}
    </span>
    <div class="interpret-block row">
      <div class="interpret col-md-4">
        {{ interpretation.Interpretation }}
      </div>
      <div class="example-setence col-md-8">
        <div class="setence">
          {{ interpretation.ExampleSentence }}
        </div>
        <div class="setence-interpret">
          {{ interpretation.ExampleSentenceExplanation }}
        </div>
      </div>
    </div>
  </li>
</template>

<script>
import PartOfSpeech from "../../constants/PartOfSpeech.js";

export default {
  props: ["interpretation"],
  methods: {},
  filters: {
    partOfSpeechToString: function(value) {
      return PartOfSpeech[value];
    },
  },
};
</script>

<style scoped>
.interpret-block {
  margin-top: 20px;
}
.interpret {
  display: inline-block;
}
.example-setence {
  display: inline-block;
}
.setence {
}
.setence-interpret {
  margin-top: 15px;
}
</style>
