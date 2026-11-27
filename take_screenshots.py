#!/usr/bin/env python3
"""Capture real output screenshots of the running Placement Dashboard app."""
import os
from playwright.sync_api import sync_playwright

BASE = "http://localhost:5019"
OUT = "/home/hatch/workspace/dotnet-experiments/Ex-9-Placement/screenshots"
os.makedirs(OUT, exist_ok=True)

SHOTS = [
    ("dashboard.png", BASE + "/Placement"),
    ("filtered-tcs.png", BASE + "/Placement?company=TCS"),
    ("details-arjun.png", BASE + "/Placement/Details/9"),
]

with sync_playwright() as p:
    browser = p.chromium.launch()
    page = browser.new_page(viewport={"width": 1280, "height": 800})
    for name, url in SHOTS:
        page.goto(url, wait_until="networkidle")
        page.wait_for_timeout(800)
        path = os.path.join(OUT, name)
        page.screenshot(path=path)
        print("saved", path, os.path.getsize(path), "bytes")
    browser.close()
print("done")
