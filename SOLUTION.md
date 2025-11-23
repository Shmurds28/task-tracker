# Debugging Issue Encountered

## Issue
Angular Material components were rendering without styling — inputs looked plain, `MatSelect` had no styling, and spacing was broken.

## Debugging
- Checked the browser console and DOM elements
  - Material classes like `mat-form-field` were present.
  - CSS variables such as `--mdc-theme-primary` were missing.
  - No Material theme CSS loaded in the Network tab.

Conclusion The Angular Material theme was not imported.

## Solution
Added the Material theme on top of `styles.scss` file

```scss
@use '@angular/material' as mat;

$my-primary: mat.define-palette(mat.$indigo-palette);
$my-accent: mat.define-palette(mat.$pink-palette, A200, A100, A400);
$my-warn: mat.define-palette(mat.$red-palette);

$my-theme: mat.define-light-theme((
  color: (
    primary: $my-primary,
    accent: $my-accent,
    warn: $my-warn
  )
));

@include mat.all-component-themes($my-theme);