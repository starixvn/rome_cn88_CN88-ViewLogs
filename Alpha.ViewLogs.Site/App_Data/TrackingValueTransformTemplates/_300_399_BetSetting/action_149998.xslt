<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="add_update_bet_limit.xslt" />
  <xsl:output method="html" indent="no" />

  <xsl:template match="r">
    <xsl:call-template name="addOrUpdateBetLimit">
      <xsl:with-param name="actionId" select="'149998'" />
    </xsl:call-template>
  </xsl:template>
</xsl:stylesheet>