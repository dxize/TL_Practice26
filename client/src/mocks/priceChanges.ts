import type { PriceChange } from "../models";
import data from "./2_hw_mock_price_changes.json";

export const priceChanges: Record<string, Record<string, PriceChange>> = data;
