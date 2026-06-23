import data from "./2_hw_mock_price_changes.json";
import type { PriceChange } from "../models";

export const priceChanges: Record<string, Record<string, PriceChange>> = data;
