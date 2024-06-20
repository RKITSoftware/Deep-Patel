export function onInitializedHandler() {
  console.log("Initiazed");
}

export function onChangeEventHandler() {
  console.log("Change event.");
}

export function onClosedEventHandler() {
  console.log("Closed");
}

export function onCopyEH() {
  console.log("Copy Event");
}

export function onCutEH() {
  console.log("Data is Cut");
}

export function onDisposingEH() {
  console.log("Element is disposing.");
}

export function onEnterKeyEH() {
  console.log("Enter key pressed.");
}

export function onFocusInEH() {
  console.log("Focus In");
}

export function onFocusOutEH() {
  console.log("Focus Out");
}

export function onKeyDownEH(e) {
  console.log("Key down", e.event.originalEvent);
}

export function onKeyUpEH(e) {
  console.log("Key Up", e.event.originalEvent);
}

export function onPasteEH() {
  console.log("Data is paste.");
}

export function onValueChangedEH(e) {
  console.log("Previous value", e.previousValue);
  console.log("Current value", e.value);
}

export function onInputEH() {
  console.log("OnInput Event Handler");
}

export function onOpenedEH() {
  console.log("OnOpened Event handler");
}

export function onOptionChangedEH() {
  console.log("Option change handler");
}

export function onContentReadyEH() {
  console.log("Content is ready.");
}

export function onItemClickEH(e) {
  console.log(e.component.option("value"));
}

export function onSelectionChangedEH(e) {
  console.log("Selection changed to", e.selectedItem);
}
