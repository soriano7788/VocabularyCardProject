<template>
  <div
    id="create-card-panel"
    class="row justify-content-center overflow-auto border bg-dark rounded"
  >
    <div class="p-3">
      <div class="row justify-content-end pb-3">
        <button
          type="button"
          class="btn-close btn-close-white"
          @click="closePanel"
        ></button>
      </div>
      <div class="row">
        <label for="vocabulary" class="col-form-label col-md-4">詞彙</label>
        <div class="col-md-8">
          <input
            type="text"
            v-model="vocabulary"
            class="form-control"
            id="vocabulary"
          />
        </div>
      </div>
      <div
        class="row"
        v-for="(interpret, index) in interpretations"
        v-bind:key="index"
      >
        <create-interpretation v-bind:interpret="interpret">
        </create-interpretation>
      </div>
      <div class="row">
        <button class="btn btn-secondary" @click="submitNewCard">送出</button>
      </div>
    </div>
  </div>
</template>

<script>
import CreateInterpretation from "../CardInterpretation/CreateCardInterpretation.vue";

export default {
  components: {
    createInterpretation: CreateInterpretation,
  },
  props: ["cardSetId"],
  data: function() {
    return {
      vocabulary: "",
      interpretations: [],
    };
  },
  methods: {
    closePanel() {
      this.$emit("closePanel");
    },
    generateInterpretForm: function() {
      // todo: 使用像 esp 那樣的 acquire new document draft 之類的方式，就不用在這邊自己 init 空格式
      const interpretation = {
        PartOfSpeech: 0,
        PhoneticSymbol: "",
        Interpretation: "",
        ExampleSentence: "",
        ExampleSentenceExplanation: "",
      };
      this.interpretations.push(interpretation);
    },
    submitNewCard: function() {
      const card = this.wrapCard();
      this.$store.dispatch("createCard", {
        cardSetId: this.cardSetId,
        card: card,
      });
      this.closePanel();
    },
    wrapCard: function() {
      const interpretations = this.wrapInterpretations();
      const card = {
        Vocabulary: this.vocabulary,
        Interpretations: interpretations,
      };
      return card;
    },
    wrapInterpretations: function() {
      const interpretations = [];
      const interprets = this.interpretations;
      for (var i = 0; i < interprets.length; i++) {
        const interpret = {
          PartOfSpeech: interprets[i].PartOfSpeech,
          PhoneticSymbol: interprets[i].PhoneticSymbol,
          Interpretation: interprets[i].Interpretation,
          ExampleSentence: interprets[i].ExampleSentence,
          ExampleSentenceExplanation: interprets[i].ExampleSentenceExplanation,
        };
        interpretations.push(interpret);
      }
      return interpretations;
    },
    closePanel: function() {
      this.$emit("closePanel");
    },
  },
  mounted: function() {
    this.generateInterpretForm();
    // scroll to bottom
    // window.scrollTo(0, document.body.scrollHeight);
  },
};
</script>

<style scoped>
#create-card-panel {
  position: fixed;
  top: 15%;
  max-height: 80%;
  max-width: 80%;
}
</style>
