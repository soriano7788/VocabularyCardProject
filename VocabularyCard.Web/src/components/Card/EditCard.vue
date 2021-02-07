<template>
  <div
    id="edit-panel"
    class="row bg-dark pt-3 pb-3 position-fixed overflow-auto justify-content-center"
  >
    <div class="row justify-content-end pb-3">
      <button
        type="button"
        class="btn-close btn-close-white"
        @click="closePanel"
      ></button>
    </div>
    <div class="mb-3 row">
      <label for="vocabulary" class="form-label col-sm-2">詞彙</label>
      <div class="col-sm-10">
        <input
          type="text"
          class="form-control"
          id="vocabulary"
          v-model="card.Vocabulary"
        />
      </div>
      <div class="row">
        <edit-card-interpret
          v-for="interpret in card.Interpretations"
          :interpret="interpret"
          :key="interpret.Id"
          @removeInterpretById="removeInterpretById($event)"
        ></edit-card-interpret>
      </div>
    </div>
    <div class="row mb-3">
      <button class="btn btn-secondary" @click="addNewInterpret">
        New interpretation
      </button>
    </div>
    <div class="row">
      <button class="btn btn-secondary" @click="submitModifiedCard">
        送出
      </button>
    </div>
  </div>
</template>

<script>
import EditCardInterpret from "../CardInterpretation/EditCardInterpret.vue";

export default {
  components: {
    editCardInterpret: EditCardInterpret,
  },
  props: ["card"],
  methods: {
    closePanel() {
      this.$emit("closeEditPanel");
    },
    addNewInterpret() {
      const interpret = {
        Id: -1 * Date.now(),
        PartOfSpeech: 0,
        PhoneticSymbol: "",
        Interpretation: "",
        ExampleSentence: "",
        ExampleSentenceExplanation: "",
      };
      this.card.Interpretations.push(interpret);
    },
    removeInterpretById(interpretId) {
      console.log("interpretId: ", interpretId);
      let index = this.getIndexOfInterpretById(interpretId);
      if (index == -1) {
        return;
      }
      this.card.Interpretations.splice(index, 1);
    },
    getIndexOfInterpretById(interpretId) {
      for (let i = 0; i < this.card.Interpretations.length; i++) {
        if (this.card.Interpretations[i].Id == interpretId) {
          return i;
        }
      }
      return -1;
    },
    submitModifiedCard() {
      this.markNewInterprets();
      console.log("submitModifiedCard: ", JSON.stringify(this.card));
      const promise = this.$store.dispatch("updateCard", this.card);
      promise
        .then(() => {
          this.closePanel();
        })
        .catch(() => {
          alert("update card fail");
        });
    },
    markNewInterprets() {
      for (let i = 0; i < this.card.Interpretations.length; i++) {
        if (this.card.Interpretations[i].Id < 0) {
          this.card.Interpretations[i].Id = 0;
        }
      }
    },
  },
};
</script>

<style scoped>
#edit-panel {
  top: 10%;
  height: 80%;
  width: 80%;
}
::-webkit-scrollbar {
  width: 12px;
}

::-webkit-scrollbar-track {
  -webkit-box-shadow: inset 0 0 6px rgb(5, 0, 0);
  border-radius: 10px;
}

::-webkit-scrollbar-thumb {
  border-radius: 10px;
  -webkit-box-shadow: inset 0 0 6px rgb(255, 255, 255);
}
</style>
